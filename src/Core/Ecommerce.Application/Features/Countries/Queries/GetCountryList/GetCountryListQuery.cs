using Ecommerce.Application.Features.Countries.VMS;
using MediatR;

namespace Ecommerce.Application.Features.Countries.Queries.GetCountryList
{
    /// <summary>
    /// Se usa IReadOnlyList porque es menos pesado, debido a que solamente es de lectura
    /// </summary>
    public class GetCountryListQuery : IRequest<IReadOnlyList<CountryVM>> 
    {

    }
}
