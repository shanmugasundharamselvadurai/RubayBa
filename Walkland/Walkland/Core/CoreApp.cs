using Acr.UserDialogs;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using Newtonsoft.Json.Serialization;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Walkland.Core.Constant;
using Xamarin.Essentials;

namespace Walkland.Core
{
    public class CoreApp : MvxApplication
    {
        private static HttpClient _publicHttpClient;
        public static HttpClient PublicHttpClient
        {
            get
            {
                if (_publicHttpClient != null)
                    return _publicHttpClient;

                _publicHttpClient = new HttpClient()
                {
                    Timeout = TimeSpan.FromMinutes(10)
                };

                return _publicHttpClient;
            }
        }

        private static HttpClient _userHttpClient;
        public static HttpClient UserHttpClient
        {
            get
            {
                if (_userHttpClient != null)
                    return _userHttpClient;

                var accessToken = SecureStorage.GetAsync("AccessToken").Result;

                if (string.IsNullOrEmpty(accessToken))
                    throw new InvalidOperationException();

                _userHttpClient = new HttpClient()
                {
                    Timeout = TimeSpan.FromMinutes(10)
                };
                _userHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                return _userHttpClient;
            }
            set
            {
                _userHttpClient = value;
            }
        }

        public static GraphQLHttpClient _publicGraphQLClient;
        public static GraphQLHttpClient PublicGraphQLClient
        {
            get
            {
                if (_publicGraphQLClient != null)
                    return _publicGraphQLClient;

                _publicGraphQLClient = new GraphQLHttpClient(Constants.GraphQLURL,
                    new NewtonsoftJsonSerializer(jsonSerializerSettings =>
                    {
                        jsonSerializerSettings.ContractResolver = new DefaultContractResolver();
                    }));

                return _publicGraphQLClient;
            }
        }

        private static GraphQLHttpClient _userGraphQLHttpClient;
        public static GraphQLHttpClient UserGraphQLClient
        {
            get
            {
                if (_userGraphQLHttpClient != null)
                    return _userGraphQLHttpClient;

                var accessToken = SecureStorage.GetAsync("AccessToken").Result;

                if (string.IsNullOrEmpty(accessToken))
                    throw new InvalidOperationException();

                _userGraphQLHttpClient = new GraphQLHttpClient(Constants.GraphQLURL,
                    new NewtonsoftJsonSerializer(jsonSerializerSettings =>
                    {
                        jsonSerializerSettings.ContractResolver = new DefaultContractResolver();
                    }));
                _userGraphQLHttpClient.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                _userGraphQLHttpClient.HttpClient.Timeout = TimeSpan.FromMinutes(5);

                return _userGraphQLHttpClient;
            }
            set
            {
                _userGraphQLHttpClient = value;
            }
        }

        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            Mvx.IoCProvider.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);
        
            RegisterCustomAppStart<AppStart>();
        }
    }
}