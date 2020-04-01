using Eventador.APP.V2.Common;
using Refit;
using System;
using System.Net.Http;
using Xamarin.Essentials;

namespace Eventador.APP.V2.Services
{
    /// <summary>
    /// Refit api
    /// </summary>
    public static class EventadorApi
    {
        /// <summary>
        /// Получить экземпляр класса для взаиможействия с API
        /// </summary>
        /// <returns></returns>
        public static IEventadorApi ResolveApi()
        {

            string accessToken = SecureStorage.GetAsync("AccessToken").Result;

            var client = new HttpClient
            {
                BaseAddress = new Uri(Constants.EventadorApiURL),
                DefaultRequestHeaders =
                {
                    { "Authorization", $"Bearer {accessToken}" }
                }
            };
            var eventadorApi = RestService.For<IEventadorApi>(client);

            return eventadorApi;
        }
    }
}
