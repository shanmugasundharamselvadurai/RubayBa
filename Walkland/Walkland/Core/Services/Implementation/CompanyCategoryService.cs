using GraphQL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Walkland.Core.Models;
using Walkland.Core.Services.Interfaces;

namespace Walkland.Core.Services.Implementation
{
    public class CompanyCategoryService : ICompanyCategoryService
    {
        public async Task<List<CompanyCategory>> GetCompanyCategories()
        {
            var request = new GraphQLRequest
            {
                Query = @"query CompanyCategoryQueries_GetCompanyCategories {
                          CompanyCategoryQueries {
                          GetCompanyCategories{
                                                Id
                                                Name
                                                CompanyCategoryStoragePath
                                                
                
                                              
                        }}}"
            };

            var response = await CoreApp.UserGraphQLClient.SendQueryAsync<JObject>(request);
            if (response.Errors != null)
            {
                throw new Exception(response.Errors[0].Message);
            }
            else
            {
                return (response.Data["CompanyCategoryQueries"]["GetCompanyCategories"]).ToObject<List<Models.CompanyCategory>>();
            }
        }
    }
}