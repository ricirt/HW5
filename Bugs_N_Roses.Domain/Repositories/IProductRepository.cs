using Bugs_N_Roses.Domain.ApplicationFilters;
using Bugs_N_Roses.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugs_N_Roses.Domain.Repositories
{
    public interface IProductRepository
    {
        void Add(Product product);
        void Update(Product product);
        void Delete(int id);
        Product Get(int id);
        IList<Product> GetAll(ApplicationParameters parameters);
        IList<Product> GetByFilter(ProductParameters parameters);
        IList<Product> SearchByName(ProductSearchParameters parameters);
    }
}
