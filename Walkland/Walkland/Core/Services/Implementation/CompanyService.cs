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

                //Query = @"query CompanyQueries_GetCompanyByCompanyCategoryIdByAlgo
                // {
                //  CompanyQueries{
                //      GetCompanyByCompanyCategoryIdByAlgo (CompanyCategoryId:" + Id + ")" +
                //      @"{     
                //              BrandName
                //              Id
                //              LogoStoragePath  
                //              Rating
                //        }}}"
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

    }
}