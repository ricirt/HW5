using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugs_N_Roses.Domain.ApplicationFilters
{
    public class ApplicationParameters
    {

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 25;
        
    }
}
