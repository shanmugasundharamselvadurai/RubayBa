using GraphQL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Walkland.Core.Models;
using Walkland.Core.Services.Interfaces;

namespace Walkland.Core.Services.Implementation
{
    public class CompanyLocationService : ICompanyLocationService
    {
        public async Task<CompanyLocation> GetNearestCompanyLocation(long CompanyId, string Latitude, string Longitude)
        {
            var request = new GraphQLRequest
            {
                Query = @"query CompanyLocationQueries_GetNearestCompanyLocation{
                          CompanyLocationQueries{
                          GetNearestCompanyLocation(CompanyId:" + CompanyId + "," + "Latitude:" + '"' + Latitude + '"' + "," + "Longitude:" + '"' + Longitude + '"' + ")" +
                       @"{
                             	 CompanyId
                                 LocationName
                                 ContactPerson
                                 MobileNumber 
                                 City
                                 AddressLine1
                                 AddressLine2                                 
                                 Landmark                                                            
                                 PinCode                                    
                                 Status
                                 Latitude
                                 Longitude
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

                return (response.Data["CompanyLocationQueries"]["GetNearestCompanyLocation"]).ToObject<CompanyLocation>();
            }

        }

        public async Task<List<CompanyLocation>> GetCompanyAllLocation(long CompanyId)
        {
            var request = new GraphQLRequest
            {
                Query = @"query CompanyLocationQueries_GetCompanyAllLocation{
                          CompanyLocationQueries{
                          GetCompanyAllLocation(CompanyId:" + CompanyId + ")" +
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
                return (response.Data["CompanyLocationQueries"]["GetCompanyAllLocation"]).ToObject<List<CompanyLocation>>();
            }
        }

        public async Task<List<CompanyLocation>> FindNearByCompanies(string Latitude, string Longitude)
        {
            var request = new GraphQLRequest
            {
                Query = @"query CompanyLocationQueries_FindNearByCompanies{
                          CompanyLocationQueries{
                          FindNearByCompanies(Latitude:" + '"' + Latitude + '"' + "," + "Longitude:" + '"' + Longitude + '"' + ")" +
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
                                    CompanyCategoryId
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
                return (response.Data["CompanyLocationQueries"]["FindNearByCompanies"]).ToObject<List<CompanyLocation>>();
            }
        }


        public async Task<List<CompanyLocation>> FindNearBySearchCompanies(string SearchValue)
        {
            var request = new GraphQLRequest
            {
                Query = @"query CompanyLocationQueries_FindNearBySearchCompanies{
                          CompanyLocationQueries{
                          FindNearBySearchCompanies(SearchValue:" + '"' + SearchValue + '"' + ")" +
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
                return (response.Data["CompanyLocationQueries"]["FindNearBySearchCompanies"]).ToObject<List<CompanyLocation>>();
            }
        }
    }

}