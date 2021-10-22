using AutoMapper;
using Bugs_N_Roses.Application.Models.UserModels;
using Bugs_N_Roses.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugs_N_Roses.Application.Services.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task<string> Login(LoginDto dto)
        {
            var user = _mapper.Map<User>(dto);
            await _signInManager.PasswordSignInAsync(user.Email,user.PasswordHash,true,user.LockoutEnabled);
            //GenerateToken();
            return "token";
        }
    }
}
