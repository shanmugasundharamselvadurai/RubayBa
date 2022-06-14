using GraphQL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Walkland.Core.Models;
using Walkland.Core.Services.Interfaces;

namespace Walkland.Core.Services.Implementation
{
    public class LatestDealService : ILatestDealService
    {

        //Latest Deal Carousel On Home Page 
        public async Task<List<LatestDeal>> GetLatestDeals()
        {
            var request = new GraphQLRequest
            {
                Query = @"query LatestDealNewQueries_GetLatestDeals {
                          LatestDealNewQueries {
                          GetLatestDeals{                               
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
                return (response.Data["LatestDealNewQueries"]["GetLatestDeals"]).ToObject<List<LatestDeal>>();
            }
        }
    }
}