using GraphQL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Walkland.Core.Models;
using Walkland.Core.Services.Interfaces;

namespace Walkland.Core.Services.Implementation
{
    public class SearchService : ISearchService
    {
        public async Task<List<CompanyProduct>> GetProductById(string searchTerm)
        {
            var request = new GraphQLRequest
            {
                Query = @"query{
                 CompanyProductQueries {
                       SearchProducts(SearchTerm:" + '"' + searchTerm + '"' + ")" +
                      @"{     Id
                              Name
                              ProductLink
                              OfferPrice
                              ProductDescription
                              ProductStoragePath
                              OriginalPrice
                              Company{
                                        BrandName
                                        Id
                              }    
                        }}}"
            };

            var response = await CoreApp.UserGraphQLClient.SendQueryAsync<JObject>(request);
            if (response.Errors != null)
            {
                throw new Exception(response.Errors[0].Message);
            }
            else
            {
                return (response.Data["CompanyProductQueries"]["SearchProducts"]).ToObject<List<CompanyProduct>>();
            }
        }

        public async Task<List<Company>> GetSearchCompany(string searchTerm)
        {
            var request = new GraphQLRequest
            {
                Query = @"query{
                 CompanyQueries {
                       SearchCompany(SearchTerm:" + '"' + searchTerm + '"' + ")" +
                      @"{     Id
                              BrandName
                              LegalName
                              LogoStoragePath
                              Location{
                                        AddressLine1
                                        AddressLine2
                                        Id
                                        Location
                                        LocationName
                              }    
                        }}}"
            };

            var response = await CoreApp.UserGraphQLClient.SendQueryAsync<JObject>(request);
            if (response.Errors != null)
            {
                throw new Exception(response.Errors[0].Message);
            }
            else
            {
                return (response.Data["CompanyQueries"]["SearchCompany"]).ToObject<List<Company>>();
            }
        }

        public async Task<List<CompanyProduct>> GetTopFiveProducts(string searchTerm)
        {
            var request = new GraphQLRequest
            {
                Query = @"query CompanyProductQueries_TopFiveProducts{
                 CompanyProductQueries {
                       TopFiveProducts(SearchTerm:" + '"' + searchTerm + '"' + ")" +
                      @"{     Id
                              Name
                              ProductLink
                              OfferPrice
                              ProductDescription
                              ProductStoragePath
                              OriginalPrice
                              Company{
                                BrandName
                              }    
                        }}}"
            };

            var response = await CoreApp.UserGraphQLClient.SendQueryAsync<JObject>(request);
            if (response.Errors != null)
            {
                throw new Exception(response.Errors[0].Message);
            }
            else
            {
                return (response.Data["CompanyProductQueries"]["TopFiveProducts"]).ToObject<List<CompanyProduct>>();
            }
        }

    }
}