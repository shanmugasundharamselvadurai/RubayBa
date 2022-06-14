using System.Threading.Tasks;

namespace Walkland.Core.Services.Interfaces
{
    public interface ICompanyRatingService
    {
        Task<string> CreateRating(long companyId, decimal Rating);

        Task<decimal> GetCompanyRating(long companyId);
    }
}