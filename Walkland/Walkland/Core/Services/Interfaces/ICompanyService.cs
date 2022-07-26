using System.Collections.Generic;
using System.Threading.Tasks;
using Walkland.Core.Models;

namespace Walkland.Core.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<List<Models.Company>> GetCompanyByCompanyCategoryId(long categoryId);
        Task<List<Models.Company>> GetCompanyByCompanySubCategoryId(long subCategoryId);
        Task<List<Models.Company>> FindNearByCompaniesByCategory(long Id, string Latitude, string Longitude);
       // Task<List<Models.Company>> GetCompanySubCategories(long Id);
    }
}