using GraphQL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Walkland.Core.Models;
using Walkland.Core.Services.Interfaces;

namespace Walkland.Core.Services.Implementation
{
    public class CompanyOfferService : ICompanyOfferService
    {
        public async Task<List<CompanyWalletTransaction>> GetCompanyEmployeeTodayWalletTransactionServices()
        {
            var request = new GraphQLRequest
            {
                Query = @"query CompanyEmployeeWalletTransactionQueries_GetEmployeeTodayWalletTransactions {                                 
                                CompanyEmployeeWalletTransactionQueries {
                                GetEmployeeTodayWalletTransactions {
                                                                Id
                                                                Amount
                                                                CreatedDateTimeUtc
                                                                FromCommissionAmount
                                                                            FromWallet{ 
                                                                                        Id
                                                                                        CompanyId
                                                                                        Amount
                                                                                        CompanyEmployeeId
                                                                                        MaxAllowedAmount
                                                                                        Company{
                                                                                                 Id
                                                                                                 BrandName    
                                                                                                          }
                                                                                       }
                                                                            FromWalletId
                                                                            ToCommissionAmount
                                                                            ToWalletId
                                                                            TransactionId
                                                                            ToWallet   {
                                                                                          Id
                                                                                          CompanyId
                                                                                          Amount
                                                                                          CompanyEmployeeId
                                                                                          MaxAllowedAmount
                                                                                          Company{
                                                                                                  Id
                                                                                                  BrandName
                                                                                                  }
                                                                                        }
                                                                              Wallet     {
                                                                                         Id
                                                                                          CompanyId
                                                                                          Amount
                                                                                          CompanyEmployeeId
                                                                                          MaxAllowedAmount
                                                                                         Company {
                                                                                                    Id
                                                                                                    BrandName
                                                                                                  }
                                                                                        }
                                                                      }
                }}"
            };

            var response = await CoreApp.UserGraphQLClient.SendQueryAsync<JObject>(request);
            return (response.Data["CompanyEmployeeTodayWalletTransactionQueries"]["GetEmployeeTodayWalletTransactions"]).ToObject<List<CompanyWalletTransaction>>();
        }

        public async Task<List<Models.CompanyOffer>> GetOfferByCompanyId(long Id)
        {
            var request = new GraphQLRequest
            {
                Query = @"query CompanyOfferQueries_GetOfferByCompanyId
                                        {
                                            CompanyOfferQueries{
                                            GetOfferByCompanyId(CompanyId: " + Id + ")" +
                                              @"{     Id
							                          Code
							                          Description
							                          PerUserLimit
							                          PercentageDiscount
							                          FixedDiscountAmount							                          
							                          MaximumDiscountAmount
							                          MinimumTransactionAmount 
                                                      ValidUpToDateTimeUtc
                                                }}}"
            };
            var response = await CoreApp.UserGraphQLClient.SendQueryAsync<JObject>(request);
            if (response.Errors != null)
            {
                throw new Exception(response.Errors[0].Message);
            }
            else
            {
                return (response.Data["CompanyOfferQueries"]["GetOfferByCompanyId"]).ToObject<List<Models.CompanyOffer>>();
            }
        }

        public async Task<List<PlatformOffer>> GetPlatformOfferByCompanyId(long Id)
        {
                var request = new GraphQLRequest
                {
                    Query = @"query PlatformOfferQueries_GetOfferByCompanyId
                                        {
                                            PlatformOfferQueries{
                                            GetOfferByCompanyId(CompanyId: " + Id + ")" +
                                                  @"{     Id
							                          Code
							                          Description
							                          PerUserLimit
							                          PercentageDiscount
							                          FixedDiscountAmount							                          
							                          MaximumDiscountAmount
							                          MinimumTransactionAmount 
                                                      ValidUpToDateTimeUtc
                                                }}}"
                };
                var response = await CoreApp.UserGraphQLClient.SendQueryAsync<JObject>(request);
                if (response.Errors != null)
                {
                    throw new Exception(response.Errors[0].Message);
                }
                else
                {
                    return (response.Data["PlatformOfferQueries"]["GetOfferByCompanyId"]).ToObject<List<PlatformOffer>>();
                }
            }

        public async Task<List<Models.Notification>> GetNotification()
        {
            var request = new GraphQLRequest
            {
                Query = @"query NotificationQueries_GetNotification
                                        {
                                            NotificationQueries{
                                            GetNotification()" +
                                              @"{     Id
							                          Header
							                          Description
							                          ValidUpToDateTimeUtc
                                                }}}"
            };
            var response = await CoreApp.UserGraphQLClient.SendQueryAsync<JObject>(request);
            if (response.Errors != null)
            {
                throw new Exception(response.Errors[0].Message);
            }
            else
            {
                return (response.Data["NotificationQueries"]["GetNotification"]).ToObject<List<Models.Notification>>();
            }
        }

    }
}
