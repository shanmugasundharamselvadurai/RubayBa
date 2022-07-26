using GraphQL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Walkland.Core.Models;
using Walkland.Core.Services.Interfaces;

namespace Walkland.Core.Services.Implementation
{
    public class CompanyService : ICompanyService
    {
        public async Task<List<Company>> FindNearByCompaniesByCategory(long Id, string Latitude, string Longitude)
        {
            var request = new GraphQLRequest
            {
                Query = @"query CompanyLocationQueries_FindNearByCompaniesByCategory{
                          CompanyLocationQueries{
                          FindNearByCompaniesByCategory(CompanyCategoryId:" + Id + "," + "Latitude:" + '"' + Latitude + '"' + "," + "Longitude:" + '"' + Longitude + '"' + ")" +
                       @"{
                             	 Id
                                 CompanyId
                                 LocationName
                                 ContactPerson
                                 MobileNumber 
                                 City
                                 AddressLine1
                                 AddressLine2                                 
                                 Landmark                                                            
                                 PinCode                                                                    
                                 Latitude
                                 Longitude 
                                  Company{
                                    Id
                                    LogoStoragePath
                                    BrandName
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
                return (response.Data["CompanyLocationQueries"]["FindNearByCompaniesByCategory"]).ToObject<List<Company>>();
            }
        }

        public async Task<List<Models.Company>> GetCompanyByCompanyCategoryId(long Id)
        {
            var request = new GraphQLRequest
            {
                Query = @"query CompanyQueries_GetCompanyByCompanyCategoryId
                 {
                  CompanyQueries{
                      GetCompanyByCompanyCategoryId (CompanyCategoryId:" + Id + ")" +
                      @"{     
                              BrandName
                              Id
                              LogoStoragePath  
                              Rating

                        }}}"
            };

            var response = await CoreApp.UserGraphQLClient.SendQueryAsync<JObject>(request);
            if (response.Errors != null)
            {
                throw new Exception(response.Errors[0].Message);
            }
            else
            {
                return (response.Data["CompanyQueries"]["GetCompanyByCompanyCategoryId"]).ToObject<List<Models.Company>>();
            }
        }

        public async Task<List<Company>> GetCompanyByCompanySubCategoryId(long subCategoryId)
        {
            var request = new GraphQLRequest
            {
                Query = @"query CompanyQueries_GetCompanyByCompanySubCategoryId
                 {
                  CompanyQueries{
                      GetCompanyByCompanySubCategoryId (CompanySubCategoryId:" + subCategoryId + ")" +
                      @"{     
                              BrandName
                              Id
                              LogoStoragePath  
                              Rating
                              PriceRange
                        }}}"

            };

            var response = await CoreApp.UserGraphQLClient.SendQueryAsync<JObject>(request);
            if (response.Errors != null)
            {
                throw new Exception(response.Errors[0].Message);
            }
            else
            {
                return (response.Data["CompanyQueries"]["GetCompanyByCompanySubCategoryId"]).ToObject<List<Models.Company>>();
            }
        }

        //public async Task<List<Company>> GetCompanySubCategories(long Id)
        //{
        //    var request = new GraphQLRequest
        //    {
        //        Query = @" query CompanySubCategoryQueries_GetCompanySubCategories
        //         {
        //          CompanySubCategoryQueries{
        //              GetCompanySubCategories (Id:8){
        //                    Id
        //                    Name
        //                    CategoryId
        //                    CompanySubCategoryStoragePath
        //                }}}"

        //    };

        //    var response = await CoreApp.UserGraphQLClient.SendQueryAsync<JObject>(request);
        //    if (response.Errors != null)
        //    {
        //        throw new Exception(response.Errors[0].Message);
        //    }
        //    else
        //    {
        //        return (response.Data["CompanyQueries"]["GetCompanySubCategories"]).ToObject<List<Models.Company>>();
        //    }
        //}
    }
}