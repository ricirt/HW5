using Bugs_N_Roses.Application.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugs_N_Roses.Application.Services.UserServices
{
    public interface IUserService
    {
        UserDTO GetById(int id);
        List<UserDTO> GetAll();
        bool Add(UserCreateDTO userCreateDTO);
        bool Update(UserUpdateDTO userUpdateDTO, int id);
        bool Delete(int id);
    }
}
