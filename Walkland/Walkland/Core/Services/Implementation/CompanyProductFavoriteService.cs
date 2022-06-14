using GraphQL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Walkland.Core.Models;
using Walkland.Core.Services.Interfaces;

namespace Walkland.Core.Services.Implementation
{
    public class CompanyProductFavoriteService : ICompanyProductFavoriteService
    {
        public async Task<List<CompanyProduct>> GetFavoriteProducts()
        {
            var request = new GraphQLRequest
            {
                Query = @"query CompanyProductFavoriteQueries_GetFavoriteProducts {
                          CompanyProductFavoriteQueries {
                          GetFavoriteProducts{
                                            Id                                                                        
                                            Name
                                            ProductLink
                                            OfferPrice
                                            OriginalPrice
                                            ProductDescription
                                            ProductStoragePath
                                            Company{
                                              BrandName
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
                return (response.Data["CompanyProductFavoriteQueries"]["GetFavoriteProducts"]).ToObject<List<CompanyProduct>>();
            }
        }

        public async Task<string> AddFavoriteProduct(long companyProductId)
        {
            var mutationRequest = new GraphQLRequest(
                              @"mutation CompanyProductFavoriteMutation_CreateFavorite ($Favourite:CompanyProductFavoriteInputObjectGraph ){
                                          CompanyProductFavoriteMutation {
                                            CreateFavorite(Favourite: $Favourite) 
                                          }
                                        }",
                   new
                   {
                       Favourite = new
                       {
                           CompanyProductId = companyProductId,

                       }
                   });

            var response = await CoreApp.UserGraphQLClient.SendMutationAsync<JObject>(mutationRequest);
            if (response.Errors != null)
            {
                throw new Exception(response.Errors[0].Message);
            }
            else
            {
                return response.Data["CompanyProductFavoriteMutation"]["CreateFavorite"].ToObject<string>();
            }
        }

        public async Task<string> RemoveFavoriteProduct(long companyProductId)
        {
            var mutationRequest = new GraphQLRequest(
                              @"mutation CompanyProductFavoriteMutation_CreateFavorite ($Favourite:CompanyProductFavoriteInputObjectGraph ){
                                          CompanyProductFavoriteMutation{
                                            DeleteFavorite(Favourite: $Favourite) 
                                          }
                                        }",
                   new
                   {
                       Favourite = new
                       {
                           CompanyProductId = companyProductId
                       }
                   });

            var response = await CoreApp.UserGraphQLClient.SendMutationAsync<JObject>(mutationRequest);

            if (response.Errors != null)
            {
                throw new Exception(response.Errors[0].Message);
            }
            else
            {
                return response.Data["CompanyProductFavoriteMutation"]["DeleteFavorite"].ToObject<string>();
            }
        }

        public async Task<bool> IsFavoriteProduct(long companyProductId)
        {
            var request = new GraphQLRequest
            {
                Query = @"query CompanyProductFavoriteQueries_IsUserFavoriteProduct{
                          CompanyProductFavoriteQueries{
                          IsUserFavoriteProduct(CompanyProductId:" + companyProductId + ")" +
                          @"}}"
            };

            var response = await CoreApp.UserGraphQLClient.SendQueryAsync<JObject>(request);
            if (response.Errors != null)
            {
                throw new Exception(response.Errors[0].Message);
            }
            else
            {
                return (response.Data["CompanyProductFavoriteQueries"]["IsUserFavoriteProduct"]).ToObject<bool>();
            }
        }
    }
}