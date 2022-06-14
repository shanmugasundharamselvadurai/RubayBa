using System.Collections.Generic;
using System.Threading.Tasks;
using Walkland.Core.Models;

namespace Walkland.Core.Services.Interfaces
{
    public interface ICompanyProductFavoriteService
    {
        Task<List<CompanyProduct>> GetFavoriteProducts();

        Task<string> AddFavoriteProduct(long companyProductId);

        Task<string> RemoveFavoriteProduct(long companyProductId);

        Task<bool> IsFavoriteProduct(long companyProductId);
    }
}