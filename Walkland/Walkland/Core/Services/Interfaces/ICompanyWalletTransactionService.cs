using System.Collections.Generic;
using System.Threading.Tasks;
using Walkland.Core.Models;

namespace Walkland.Core.Services.Interfaces
{
    public interface ICompanyWalletTransactionService
    {
        Task<List<CompanyWalletTransaction>> GetCompanyEmployeeWalletTransactionServices();

        Task<CompanyWalletTransaction> ReturnPoints(decimal amount, string reason);

        Task<CompanyWalletTransaction> SendPoints(string accountNumber, decimal amount, string reason, long? companyLocationId,string TransNo);

        Task<List<CompanyWalletTransaction>> GetCompanyEmployeeTodayWalletTransactionServices();
    }
}