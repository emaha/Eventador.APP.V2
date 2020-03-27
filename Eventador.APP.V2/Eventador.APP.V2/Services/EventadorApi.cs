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
            string accessToken = SecureStorage.GetAsync("AccessToken").Result;

            var client = new HttpClient
            {
                BaseAddress = new Uri(Constants.EventadorApiURL),
                DefaultRequestHeaders =
                {
                    { "Authorization", $"Bearer {accessToken}" }
                }
            };
            eventadorApi = RestService.For<IEventadorApi>(client);
        }

        /// <summary>
        /// Обновить заголовок Refit с новым токеном
        /// </summary>
        public static void RefreshHttpClient()
        {
            Init();
        }

        /// <summary>
        /// Получить экземпляр класса для взаиможействия с API
        /// </summary>
        /// <returns></returns>
        public static IEventadorApi ResolveApi()
        {
            // TODO: Херня полная. Придумать как прикрутить нормальный DI
            return eventadorApi;
        }
    }
}
