using GraphQL;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using Walkland.Core.Services.Interfaces;

namespace Walkland.Core.Services.Implementation
{
    public class CompanyRatingService : ICompanyRatingService
    {
        public async Task<string> CreateRating(long companyId, decimal Rating)
        {

            var mutationRequest = new GraphQLRequest(
                              @"mutation CompanyRatingInputObjectGraph_CreateRating ($Rating:CompanyRatingInputObjectGraph ){
                                      CompanyRatingMutation{
                                        CreateRating(Rating: $Rating) 
                                      }
                                    }",
                   new
                   {
                       Rating = new
                       {
                           CompanyId = companyId,
                           Rating = Rating
                       }
                   });

            var response = await CoreApp.UserGraphQLClient.SendMutationAsync<JObject>(mutationRequest);

            if (response.Errors != null)
            {
                throw new Exception(response.Errors[0].Message);
            }
            else
            {
                return response.Data["CompanyRatingMutation"]["CreateRating"].ToObject<string>();
            }
        }

        public async Task<decimal> GetCompanyRating(long companyId)
        {
            var request = new GraphQLRequest
            {
                Query = @"query CompanyRatingQueries_GetRatingByCompanyId{
                          CompanyRatingQueries{
                          GetRatingByCompanyId(CompanyId:" + companyId + ")" +
                       @"}}"

            };

            var response = await CoreApp.UserGraphQLClient.SendQueryAsync<JObject>(request);
            if (response.Errors != null)
            {
                throw new Exception(response.Errors[0].Message);
            }
            else
            {
                return (response.Data["CompanyRatingQueries"]["GetRatingByCompanyId"]).ToObject<decimal>();
            }
        }
    }
}