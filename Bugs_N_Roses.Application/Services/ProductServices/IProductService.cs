using Bugs_N_Roses.Application.Models.ProductModels;
using Bugs_N_Roses.Domain.ApplicationFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugs_N_Roses.Application.Services.ProductServices
{
    public interface IProductService
    {
        ProductDTO GetById(int id);
        List<ProductDTO> GetAll(ApplicationParameters parameters);
        bool Add(ProductCreateDTO productCreateDTO);
        bool Update(ProductUpdateDTO productUpdateDTO, int id);
        bool Delete(int id);
        List<ProductDTO> GetByFilter(ProductParameters parameters);
        List<ProductDTO> SearchByName(ProductSearchParameters parameters);
    }
}
