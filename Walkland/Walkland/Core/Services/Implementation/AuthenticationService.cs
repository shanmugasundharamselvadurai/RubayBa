using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using Newtonsoft.Json;
using Walkland;
using Walkland.Core;
using Walkland.Core.Constant;
using Walkland.Core.Exceptions;
using Walkland.Core.Services.Interfaces;
using Xamarin.Essentials;

namespace Core.Services.Implementation
{
    public class AuthenticationService : IAuthenticationService
    {
        public async Task SignInAsync(LoginRequestDto user)
        {
            var response = await CoreApp.PublicHttpClient.PostAsync(Constants.BaseURL + "/Account/Login", new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new AppException(content);
            }

            var token = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new AppException("");
            }

            var jwt = new JwtSecurityToken(token);

            var firstName = jwt.Claims.First(c => c.Type == "FirstName").Value;
            var lastName = jwt.Claims.First(c => c.Type == "LastName").Value;
            var email = jwt.Claims.First(c => c.Type == ClaimTypes.Email).Value;
            var name = jwt.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var role = jwt.Claims.First(c => c.Type == ClaimTypes.Role).Value;
            var applicationUserId = jwt.Claims.First(c => c.Type == "ApplicationUserId").Value;
            var companyId = jwt.Claims.First(c => c.Type == "CompanyId").Value;
            var commpanyWalletId = jwt.Claims.First(c => c.Type == "CommpanyWalletId").Value;
            var tokenExpiredOn = jwt.Claims.First(c => c.Type == "exp").Value;
            var tokenExpiredDate = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(jwt.Claims.First(c => c.Type == "exp").Value));
            var uname = jwt.Claims.First(c => c.Type == "UName").Value;
            var accno = jwt.Claims.First(c => c.Type == "AccNo").Value;

            await SecureStorage.SetAsync("AccessToken", token);
            await SecureStorage.SetAsync("FirstName", firstName);
            await SecureStorage.SetAsync("LastName", lastName);
            await SecureStorage.SetAsync("Email", email);
            await SecureStorage.SetAsync("UserName", name);
            await SecureStorage.SetAsync("Role", role);
            await SecureStorage.SetAsync("ApplicationUserId", applicationUserId);
            await SecureStorage.SetAsync("CompanyId", companyId);
            await SecureStorage.SetAsync("CommpanyWalletId", commpanyWalletId);
            await SecureStorage.SetAsync("UName", uname);
            await SecureStorage.SetAsync("AccNo", accno);

        }

        public void SignOut()
        {
            SecureStorage.RemoveAll();
            CoreApp.UserGraphQLClient = null;
            CoreApp.UserHttpClient = null;
        }

        public async Task ChangePasswordAsync(ChangePasswordRequestDto requestDto)
        {
            var response = await CoreApp.UserHttpClient.PostAsync(Constants.BaseURL + "/Account/ChangePassword", new StringContent(JsonConvert.SerializeObject(requestDto), Encoding.UTF8, "application/json"));

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new AppException(content);
            }
        }

        public async Task<bool> ValidateToken()
        {
            var token = await SecureStorage.GetAsync("AccessToken");
            if (!string.IsNullOrWhiteSpace(token))
            {
                var jwt = new JwtSecurityToken(token);
                var tokenExpiredOn = jwt.Claims.First(c => c.Type == "exp").Value;
                var tokenExpiredDate = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(jwt.Claims.First(c => c.Type == "exp").Value));

                if (DateTime.Now.Date >= tokenExpiredDate.Date)
                {
                    SignOut();
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public async Task GuestRegisterSignInAsync(GuestUserRequestDto user)
        {
            var response = await CoreApp.PublicHttpClient.PostAsync(Constants.BaseURL + "/Account/GuestUserRegistration", new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));
            //var response = await CoreApp.PublicHttpClient.PostAsync("http://10.0.2.2:5000/Account/GuestUserRegistration", new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new AppException(content);
            }

            var token = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new AppException("");
            }

            var jwt = new JwtSecurityToken(token);

            var firstName = jwt.Claims.First(c => c.Type == "FirstName").Value;
            var lastName = jwt.Claims.First(c => c.Type == "LastName").Value;
            var email = jwt.Claims.First(c => c.Type == ClaimTypes.Email).Value;
            var name = jwt.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var role = jwt.Claims.First(c => c.Type == ClaimTypes.Role).Value;
            var applicationUserId = jwt.Claims.First(c => c.Type == "ApplicationUserId").Value;
            var companyId = jwt.Claims.First(c => c.Type == "CompanyId").Value;
            var commpanyWalletId = jwt.Claims.First(c => c.Type == "CommpanyWalletId").Value;
            var tokenExpiredOn = jwt.Claims.First(c => c.Type == "exp").Value;
            var tokenExpiredDate = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(jwt.Claims.First(c => c.Type == "exp").Value));

            await SecureStorage.SetAsync("AccessToken", token);
            await SecureStorage.SetAsync("FirstName", firstName);
            await SecureStorage.SetAsync("LastName", lastName);
            await SecureStorage.SetAsync("Email", email);
            await SecureStorage.SetAsync("UserName", name);
            await SecureStorage.SetAsync("Role", role);
            await SecureStorage.SetAsync("ApplicationUserId", applicationUserId);
            await SecureStorage.SetAsync("CompanyId", companyId);
            await SecureStorage.SetAsync("CommpanyWalletId", commpanyWalletId);
            await SecureStorage.SetAsync("TokenExpiredOn", tokenExpiredOn);
        }

        public async Task InsertUserCategory(UserCategoryRequestDto user)
        {
            var response = await CoreApp.PublicHttpClient.PostAsync(Constants.BaseURL + "/Account/InsertUserCategory", new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));

        }

        public async Task InsertUserCompanyCategory(UserCompanyRequestDto user)
        {
            var response = await CoreApp.PublicHttpClient.PostAsync(Constants.BaseURL + "/Account/InsertUserCompanyCategory", new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));

        }
    }
}