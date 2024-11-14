using AutoMapper;
using Ecommerce.Application.Exceptions;
using Ecommerce.Application.Features.Products.Queries.VMS;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;

namespace Ecommerce.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ProductVM>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductVM> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
           var productUpdate = await _unitOfWork.Repository<Product>().GetByIdAsync(request.ProductId);

            if(productUpdate is null)
            {
                throw new NotFoundException(nameof(Product), request.ProductId);
            }

            productUpdate.Status = productUpdate.Status == ProductStatus.Inactivo
                ? ProductStatus.Activo : ProductStatus.Inactivo;

            await _unitOfWork.Repository<Product>().UpdateAsync(productUpdate);

            return _mapper.Map<ProductVM>(productUpdate);
        }
    }
}
