using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Walkland.Core.Services.Interfaces
{
    public interface ICompanySubCategoryService
    {
        Task<List<Models.CompanySubCategory>> GetCompanySubCategories(long Id);
    }
}
