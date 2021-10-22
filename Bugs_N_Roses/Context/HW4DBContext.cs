using Bugs_N_Roses.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugs_N_Roses.Infrastructure.Context
{
    public class HW4DBContext : IdentityDbContext
    {
        public HW4DBContext(DbContextOptions<HW4DBContext> options):base()
        {

        }
    }
}
