using System.Collections.Generic;
using System.Threading.Tasks;
using Walkland.Core.Models;

namespace Walkland.Core.Services.Interfaces
{
    public interface ICompanyProductService
    {
        Task<List<CompanyProduct>> GetProductCategoryId(long id);

        Task<List<CompanyProduct>> GetProductByCompanyId(long id);

        Task<List<CompanyProduct>> GetProductById(long id);
    }
}