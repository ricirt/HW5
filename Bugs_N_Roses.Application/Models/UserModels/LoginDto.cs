using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugs_N_Roses.Application.Models.UserModels
{
    public class LoginDto
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
    }
}
