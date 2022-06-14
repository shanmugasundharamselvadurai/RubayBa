using GraphQL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Walkland.Core.Models;
using Walkland.Core.Services.Interfaces;

namespace Walkland.Core.Services.Implementation
{
    public class CompanyWalletTransactionService : ICompanyWalletTransactionService
    {
        public async Task<List<CompanyWalletTransaction>> GetCompanyEmployeeWalletTransactionServices()
        {
            var request = new GraphQLRequest
            {
                Query = @"query CompanyEmployeeWalletTransactionQueries_GetEmployeeWalletTransactions {                                 
                                CompanyEmployeeWalletTransactionQueries {
                                GetEmployeeWalletTransactions {
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
            return (response.Data["CompanyEmployeeWalletTransactionQueries"]["GetEmployeeWalletTransactions"]).ToObject<List<CompanyWalletTransaction>>();
        }

        public async Task<CompanyWalletTransaction> SendPoints(string accountNumber, decimal amount, string reason, long? companyLocationId,string TransNo)
        {

            var mutationRequest = new GraphQLRequest(
                              @"mutation PaymentMutations_CreatePayment($Payment: PaymentDtoInputObjectGraph) {
                                             PaymentMutations {
                                               CreatePayment(Payment: $Payment) {
                                                 Amount
                                                 CreatedDateTimeUtc
                                                 FromCommissionAmount
                                                 FromWalletId
                                                 Id
                                                 ToCommissionAmount
                                                 ToWalletId
                                                 TransactionId
                                                 
                                               }
                                             }
                                           }",
                   new
                   {
                           Payment = new
                           {
                               AccountNumber = accountNumber,
                               Amount = amount,
                               Reason = reason,
                               CompanyLocationId = companyLocationId,
                               TransNo = TransNo
                           }
                   });

            var response = await CoreApp.UserGraphQLClient.SendMutationAsync<JObject>(mutationRequest);

            if (response.Errors != null)
            {
                throw new Exception(response.Errors[0].Message);
            }       
            else
            {
                return response.Data["PaymentMutations"]["CreatePayment"].ToObject<CompanyWalletTransaction>();
            }
        }

        //Variable '$Payment' is invalid. Unrecognized input fields 'TransNo' for type 'PaymentDtoInputObjectGraph'.

        public async Task<CompanyWalletTransaction> ReturnPoints(decimal amount, string reason)
        {

            var mutationRequest = new GraphQLRequest(
                              @"mutation ParentCompanyPaymentMutation_ReturnToParentCompany($Payment: ParentCompanyPaymentDtoInputObjectGraph) {
                                             ParentCompanyPaymentMutation {
                                               ReturnToParentCompany(Payment: $Payment) {
                                                 Amount
                                                 CreatedDateTimeUtc
                                                 FromCommissionAmount
                                                 FromWalletId
                                                 Id
                                                 ToCommissionAmount
                                                 ToWalletId
                                                 TransactionId
                                               }
                                             }
                                           }",
                   new
                   {
                       Payment = new
                       {
                           Amount = amount,
                           Reason = reason
                       }
                   });

            var response = await CoreApp.UserGraphQLClient.SendMutationAsync<JObject>(mutationRequest);

            if (response.Errors != null)
            {
                throw new Exception(response.Errors[0].Message);
            }
            else
            {
                return response.Data["ParentCompanyPaymentMutation"]["ReturnToParentCompany"].ToObject<CompanyWalletTransaction>();
            }
        }

        //notification Today Transation
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
    }
}