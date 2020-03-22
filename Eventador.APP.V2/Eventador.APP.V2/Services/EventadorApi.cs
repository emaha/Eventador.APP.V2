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
        private static IEventadorApi eventadorApi;
        static EventadorApi()
        {
            Init();
        }

        /// <summary>
        /// Инициализация
        /// </summary>
        private static void Init()
        {
            string authHeader = SecureStorage.GetAsync("AccessToken").Result;

            var client = new HttpClient
            {
                BaseAddress = new Uri(Constants.EventadorApiURL),
                DefaultRequestHeaders =
                {
                    { "Authorization", $"Bearer {authHeader}" }
                }
            };
            eventadorApi = RestService.For<IEventadorApi>(client);
        }

        /// <summary>
        /// Обновить заголовок Refit с новым токеном
        /// </summary>
        public static void RefreshToken()
        {
            Init();
        }

        /// <summary>
        /// Получить экземпляр класса для взаиможействия с API
        /// </summary>
        /// <returns></returns>
        public static IEventadorApi ResolveApi()
        {
            return eventadorApi;
        }
    }
}
