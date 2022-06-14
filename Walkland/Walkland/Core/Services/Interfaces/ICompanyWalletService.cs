using System.Collections.Generic;
using System.Threading.Tasks;
using Walkland.Core.Models;

namespace Walkland.Core.Services.Interfaces
{
    public interface ICompanyWalletService
    {
        Task<CompanyWallet> GetWalletBalanceForCustomerUser();

        Task<Company> GetCompanyByAccountNumber(string accountNumber);
        Task<Company> GetCompanyById(long Id);
        Task<Models.CompanyWallet> GetCompanyByQR(string accountNumber);
    }
}