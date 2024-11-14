using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Countries.VMS
{
    public  class CountryVM
    {
        public int Id { get; set; }
        public string?  Name { get; set; }
        /// <summary>
        /// Abreviatura del país
        /// </summary>
        public string? Iso2 { get; set; }

        /// <summary>
        /// Abreviatura del país
        /// </summary>
        public string? Iso3 { get; set; }
    }
}
