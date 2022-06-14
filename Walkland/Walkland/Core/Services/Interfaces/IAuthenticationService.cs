using System.Threading.Tasks;
using Core.Models;
using Walkland.Core.Services.Implementation;

namespace Walkland.Core.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task SignInAsync(LoginRequestDto user);

        void SignOut();

        Task ChangePasswordAsync(ChangePasswordRequestDto requestDto);

        Task<bool> ValidateToken();

        Task GuestRegisterSignInAsync(GuestUserRequestDto user);

        Task InsertUserCategory(UserCategoryRequestDto user);

        Task InsertUserCompanyCategory(UserCompanyRequestDto user);
    }
}