using Bugs_N_Roses.Application.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugs_N_Roses.Application.Services.AuthServices
{
    public interface IAuthService
    {
        Task<string> Login(LoginDto dto);
    }
}
