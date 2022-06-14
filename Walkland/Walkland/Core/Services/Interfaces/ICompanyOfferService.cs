using System.Collections.Generic;
using System.Threading.Tasks;
using Walkland.Core.Models;

namespace Walkland.Core.Services.Interfaces
{
    public interface ICompanyOfferService
    {
        Task<List<Models.CompanyOffer>> GetOfferByCompanyId(long Id);
        Task<List<Models.PlatformOffer>> GetPlatformOfferByCompanyId(long Id);
        Task<List<Models.Notification>> GetNotification();
  }
}