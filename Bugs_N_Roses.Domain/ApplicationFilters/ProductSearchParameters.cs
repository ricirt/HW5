using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugs_N_Roses.Domain.ApplicationFilters
{
    public class ProductSearchParameters : ApplicationParameters
    {
        public string SearchString { get; set; }
    }
}
