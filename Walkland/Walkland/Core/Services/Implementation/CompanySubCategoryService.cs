using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQL;
using Newtonsoft.Json.Linq;
using Walkland.Core.Models;

namespace Walkland.Core.Services.Implementation
{
    public class CompanySubCategoryService
    {
        public async Task<List<CompanySubCategory>> GetCompanySubCategories(long Id)
        {
            var request = new GraphQLRequest
            {
                Query = @" query CompanySubCategoryQueries_GetCompanySubCategories
                 {
                  CompanySubCategoryQueries{
                      GetCompanySubCategories (Id:" + Id + ")" +
                  @"{
                            Id
                            Name
                            CategoryId
                            CompanySubCategoryStoragePath
                   }}}"
            };

            var response = await CoreApp.UserGraphQLClient.SendQueryAsync<JObject>(request);
            if (response.Errors != null)
            {
                throw new Exception(response.Errors[0].Message);
            }
            else
            {
                return (response.Data["CompanySubCategoryQueries"]["GetCompanySubCategories"]).ToObject<List<Models.CompanySubCategory>>();
            }
        }
    }
}
