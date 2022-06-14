using GraphQL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Walkland.Core.Models;
using Walkland.Core.Services.Interfaces;

namespace Walkland.Core.Services.Implementation
{
    public class CompanyWalletService : ICompanyWalletService
    {
        public async Task<Models.CompanyWallet> GetWalletBalanceForCustomerUser()
        {
            var request = new GraphQLRequest
            {
                Query = @"query CompanyEmployeeWalletQueries_GetEmployeeWalletDetails
                 {
                  CompanyEmployeeWalletQueries {
                                        GetEmployeeWalletDetails {
                                                                  Id
                                                                  CompanyId        
                                                                  CompanyEmployeeId
                                                                  MaxAllowedAmount
                                                                  Amount  
                                                                  Company {
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
            return response.Data["CompanyEmployeeWalletQueries"]["GetEmployeeWalletDetails"].ToObject<Models.CompanyWallet>();
        }

        public async Task<Models.Company> GetCompanyByAccountNumber(string accountNumber)
        {
            var request = new GraphQLRequest
            {
                Query = @"query CompanyQueries_GetCompanyByAccountNumber
                 {
                  CompanyQueries{
                      GetCompanyByAccountNumber (AccountNumber:" + '"' + accountNumber + '"' + ")" +
                      @"{   
                            Id
                            BrandName
                            LegalName

                        }}}"

            };

            var response = await CoreApp.UserGraphQLClient.SendQueryAsync<JObject>(request);
            if (response.Errors != null)
            {
                throw new Exception(response.Errors[0].Message);
            }
            else
            {
                return response.Data["CompanyQueries"]["GetCompanyByAccountNumber"].ToObject<Models.Company>();
            }
        }

        public async Task<Models.Company> GetCompanyById(long Id)
        {
            var request = new GraphQLRequest
            {
                Query = @"query CompanyQueries_GetCompanyById
                 {
                  CompanyQueries{
                      GetCompanyById (Id:" + Id + ")" +
                      @"{
                          BrandName
                          LegalName
                        }}}"
            };

            var response = await CoreApp.UserGraphQLClient.SendQueryAsync<JObject>(request);
            if (response.Errors != null)
            {
                throw new Exception(response.Errors[0].Message);
            }
            else
            {
                return response.Data["CompanyQueries"]["GetCompanyById"].ToObject<Models.Company>();
            }
        }


        public async Task<Models.CompanyWallet> GetCompanyByQR(string accountNumber)
        {
            var request = new GraphQLRequest
            {
                Query = @"query CompanyQueries_GetCompanyByQR
                 {
                  CompanyQueries{
                      GetCompanyByQR (AccountNumber:" + '"' + accountNumber + '"' + ")" +
                      @"{   
                            AccountNumber
                        }}}"

            };

            var response = await CoreApp.UserGraphQLClient.SendQueryAsync<JObject>(request);
            if (response.Errors != null)
            {
                throw new Exception(response.Errors[0].Message);
            }
            else
            {
                return response.Data["CompanyQueries"]["GetCompanyByQR"].ToObject<Models.CompanyWallet>();
            }
        }


    }
}