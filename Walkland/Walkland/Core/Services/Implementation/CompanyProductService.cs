using GraphQL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Walkland.Core.Models;
using Walkland.Core.Services.Interfaces;

namespace Walkland.Core.Services.Implementation
{
    public class CompanyProductService : ICompanyProductService
    {
        public async Task<List<CompanyProduct>> GetProductCategoryId(long Id)
        {
            var request = new GraphQLRequest
            {
                Query = @"query CompanyProductQueries_GetProductsByCompanyCategoryId
                 {
                  CompanyProductQueries{
                      GetProductsByCompanyCategoryId (CompanyCategoryId:" + Id + ")" +
                      @"{    
                               Id
                               Name
                               ProductDescription
                               ProductStoragePath
                               ProductLink
                               Company{
                                       Id
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
                return (response.Data["CompanyProductQueries"]["GetProductsByCompanyCategoryId"]).ToObject<List<CompanyProduct>>();
            }
        }

        //// Product List
        public async Task<List<CompanyProduct>> GetProductByCompanyId(long Id)
        {
            var request = new GraphQLRequest
            {
                Query = @"query CompanyProductQueries_GetProductsByCompanyId
                 {
                  CompanyProductQueries{
                      GetProductsByCompanyId (CompanyId:" + Id + ")" +
                      @"{     Name
                              OfferPrice
                              OriginalPrice
                              Id
                              ProductLink
                              ProductDescription
                              ProductStoragePath
                              Company{
                                Id
                                BrandName
                                LogoStoragePath
                                Website
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

                return (response.Data["CompanyProductQueries"]["GetProductsByCompanyId"]).ToObject<List<CompanyProduct>>();
            }
        }

        // Product Detail Page
        public async Task<List<CompanyProduct>> GetProductById(long Id)
        {
            var request = new GraphQLRequest
            {
                Query = @"query CompanyProductQueries_GetProductDetailsByProductId
                 {
                  CompanyProductQueries{
                      GetProductDetailsByProductId (ProductId:" + Id + ")" +
                      @"{     Name
                              OfferPrice
                              Id
                              OriginalPrice
                              ProductLink
                              ProductDescription
                              ProductStoragePath
                              Company{
                                Id
                                BrandName
                                LogoStoragePath
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
                return (response.Data["CompanyProductQueries"]["GetProductDetailsByProductId"]).ToObject<List<CompanyProduct>>();
            }
        }

        //public Task<List<CompanyProduct>> GetProductByCompanyId(long id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}