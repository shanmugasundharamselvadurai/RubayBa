using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Services.Interfaces;
using GraphQL;
using Newtonsoft.Json.Linq;
using Walkland.Core;
using Walkland.Core.Models;

namespace Core.Services.Implementation
{
    public class NotificationService: INotificationService
    {
        public async Task<List<Notification>> GetNotification()
        {
            var request = new GraphQLRequest
            {
                Query = @"query NotificationQueries_GetNotification {
                          NotificationQueries {
                          GetNotification{
                                       Id
							           Header
							           Description
							           ValidUpToDateTimeUtc
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
                return (response.Data["NotificationQueries"]["GetNotification"]).ToObject<List<Notification>>();
            }
        }
    }
}
