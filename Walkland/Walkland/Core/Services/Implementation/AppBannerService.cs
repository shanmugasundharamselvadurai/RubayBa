using GraphQL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Walkland.Core.Models;
using Walkland.Core.Services.Interfaces;

namespace Walkland.Core.Services.Implementation
{
    public class AppBannerService : IAppBannerService
    {
        public async Task<List<AppBanner>> GetAppBanners()
        {
            var request = new GraphQLRequest
            {
                Query = @"query AppBannerQueries_GetAppBanners {
                          AppBannerQueries {
                          GetAppBanners{
                                       PictureStoragePath                                     
                                       Description
                                       URlForLandingPage
                                                    Company {
                                                    BrandName
                                                    Id
                                                    CompanyCategoryId
                                                    LogoStoragePath
                                                    Rating
                                                    }
                                  }
                        }}"
            };

            var response = await CoreApp.UserGraphQLClient.SendQueryAsync<JObject>(request);
            if (response.Errors != null)
            {
                throw new Exception(response.Errors[0].Message);
            }
            else
            {
                return (response.Data["AppBannerQueries"]["GetAppBanners"]).ToObject<List<AppBanner>>();
            }
        }
    }
}