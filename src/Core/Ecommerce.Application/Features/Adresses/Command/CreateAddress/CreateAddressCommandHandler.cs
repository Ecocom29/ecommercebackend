using AutoMapper;
using Ecommerce.Application.Features.Adresses.VMS;
using Ecommerce.Application.Identity;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;

namespace Ecommerce.Application.Features.Adresses.Command.CreateAddress
{
    public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, AddressVM>
    {
        private readonly IAuthService _authService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateAddressCommandHandler(IAuthService authService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _authService = authService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AddressVM> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            var addressRecord = await _unitOfWork.Repository<Address>().GetEntityAsync(
                x=>x.Username == _authService.GetSessionUser(),
                null,
                false
            );

            if(addressRecord is null)
            {
                addressRecord = new Address
                {
                    Direccion = request.Direccion,
                    Ciudad = request.Ciudad,
                    Departamento = request.Departamento,
                    CodigoPostal = request.CodigoPostal,
                    Pais = request.Pais,
                    Username = _authService.GetSessionUser()
                };

                _unitOfWork.Repository<Address>().AddEntity(addressRecord);
            }
            else
            {
                addressRecord.Direccion = request.Direccion;
                addressRecord.Ciudad = request.Ciudad;
                addressRecord.Departamento = request.Departamento;
                addressRecord.CodigoPostal = request.CodigoPostal;
                addressRecord.Pais = request.Pais;
            }

            await _unitOfWork.Complete();

            return _mapper.Map<AddressVM>(addressRecord);
        }
    }
}
