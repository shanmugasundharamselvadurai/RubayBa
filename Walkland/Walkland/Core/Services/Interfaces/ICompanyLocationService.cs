using System.Collections.Generic;
using System.Threading.Tasks;
using Walkland.Core.Models;

namespace Walkland.Core.Services.Interfaces
{
    public interface ICompanyLocationService
    {
        Task<CompanyLocation> GetNearestCompanyLocation(long CompanyId, string Latitude, string Longitude);
        
        Task<List<CompanyLocation>> FindNearByCompanies(string Latitude, string Longitude);
        Task<List<CompanyLocation>> GetCompanyAllLocation(long CompanyId);
        Task<List<CompanyLocation>> FindNearBySearchCompanies(string searchTerm);
    }
}