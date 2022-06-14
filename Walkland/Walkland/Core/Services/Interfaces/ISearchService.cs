using System.Collections.Generic;
using System.Threading.Tasks;
using Walkland.Core.Models;

namespace Walkland.Core.Services.Interfaces
{
    public interface ISearchService
    {
        Task<List<CompanyProduct>> GetProductById(string searchTerm);

        Task<List<CompanyProduct>> GetTopFiveProducts(string searchTerm);
        Task<List<Company>> GetSearchCompany(string searchTerm);

    }
}