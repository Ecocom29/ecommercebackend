using AutoMapper;
using Ecommerce.Application.Features.Products.Queries.VMS;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;

namespace Ecommerce.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductVM>
    {   
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductVM> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var productEntity = _mapper.Map<Product>(request); //Transformar a tipo Product

            await _unitOfWork.Repository<Product>().AddAsync(productEntity);

            if((request.ImageURLs is not null) && request.ImageURLs.Count > 0 )
            {
                request.ImageURLs.Select(c => { c.ProductId = productEntity.Id; return c; }).ToList(); //Asignar Ids a los nuevos productos

                var images = _mapper.Map<List<Images>>(request.ImageURLs);
                _unitOfWork.Repository<Images>().AddRange(images);
                await _unitOfWork.Complete();

            }

            return _mapper.Map<ProductVM>(productEntity);
        }
    }
}
