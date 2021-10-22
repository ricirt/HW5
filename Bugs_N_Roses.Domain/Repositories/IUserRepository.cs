using Bugs_N_Roses.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugs_N_Roses.Domain.Repositories
{
    public interface IUserRepository
    {
        void Add(User user);
        void Update(User user);
        void Delete(int id);
        User Get(int id);
        IList<User> GetAll();
    }
}
