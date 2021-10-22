using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugs_N_Roses.Domain.ApplicationFilters
{
    public class ProductParameters : ApplicationParameters
    {
        public decimal MinPrice { get; set; } = 0;
        public decimal MaxPrice { get; set; } = Decimal.MaxValue;
    }
}
