using AutoMapper;
using Ecommerce.Application.Exceptions;
using Ecommerce.Application.Features.Products.Queries.VMS;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;

namespace Ecommerce.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProducCommandHandler : IRequestHandler<UpdateProducCommand, ProductVM>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProducCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductVM> Handle(UpdateProducCommand request, CancellationToken cancellationToken)
        {
            var productUpdate = await _unitOfWork.Repository<Product>().GetByIdAsync(request.Id);

            if (productUpdate is null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }

            _mapper.Map(request, productUpdate, typeof(UpdateProducCommand), typeof(Product));

            await _unitOfWork.Repository<Product>().UpdateAsync(productUpdate);

            if ((request.ImagesUrls is not null) && request.ImagesUrls.Count > 0)
            {
                var imagesToRemove = await _unitOfWork.Repository<Images>().GetAsync(
                    x => x.ProductId == request.Id
                );

                _unitOfWork.Repository<Images>().DeleteRange(imagesToRemove); //Conjunto de imagens eliminadas

                request.ImagesUrls!.Select(c => { c.ProductId = request.Id; return c; }).ToList();

                var images = _mapper.Map<List<Images>>(request.ImagesUrls);

                _unitOfWork.Repository<Images>().AddRange(images); //Agregar nuevas imagenes

                await _unitOfWork.Complete();
            }

            return _mapper.Map<ProductVM>(productUpdate);
        }
    }
}
