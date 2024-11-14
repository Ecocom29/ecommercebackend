using Ecommerce.Application.Features.Adresses.VMS;
using MediatR;

namespace Ecommerce.Application.Features.Adresses.Command.CreateAddress
{
    public class CreateAddressCommand: IRequest<AddressVM>
    {
        public string? Direccion {  get; set; }
        public string? Ciudad { get; set; }
        public string? Departamento { get; set; }
        public string? CodigoPostal { get; set; }
        public string? Pais {  get; set; }
    }
}
