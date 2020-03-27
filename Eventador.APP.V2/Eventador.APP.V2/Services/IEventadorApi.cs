using Eventador.APP.V2.Models;
using Eventador.APP.V2.Requests;
using Refit;
using System.Threading.Tasks;

namespace Eventador.APP.V2.Services
{
    [Headers("Accept: application/json")]
    public interface IEventadorApi
    {
        #region Auth

        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Post("/Auth/SignIn")]
        Task<TokenModel> SignIn(CredentialsRequest request);

        /// <summary>
        /// Выход
        /// </summary>
        /// <returns></returns>
        [Post("/Auth/SignOut")]
        Task SignOut();

        /// <summary>
        /// Обновить токен
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Post("/Auth/RefreshToken")]
        Task<TokenModel> RefreshToken(RefreshTokenRequest request);

        #endregion

        #region Users
        /// <summary>
        /// Полуение пользователей
        /// </summary>
        /// <returns></returns>
        [Get("/Users")]
        Task<UserResponseModel> GetUser();

        /// <summary>
        /// Создать пользователя
        /// </summary>
        /// <returns></returns>
        [Post("/Users")]
        Task CreateUser();

        #endregion 

        #region Events
        /// <summary>
        /// Получить детальную модель события
        /// </summary>
        /// <returns></returns>
        [Get("/Events/{id}")]
        Task<EventModel> GetEventById(long id);

        /// <summary>
        /// Получение событий по региону
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Get("/Events/Region/{id}")]
        Task<SmallEventModel[]> GetEventsByRegion(long id);


        /// <summary>
        /// Получение событий по автору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Get("/Events/Author/{id}")]
        Task<SmallEventModel[]> GetEventsByAuthor(long id);

        /// <summary>
        /// Создание события
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Post("/Events")]
        Task CreateEvent(EventCreateRequest request);

        /// <summary>
        /// Обновление события
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Put("/Events)")]
        Task UpdateEvent(EventUpdateRequest request);

        /// <summary>
        /// Завершить событие
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Post("/Events/Finish/{id}")]
        Task FinishEvent(long id);

        /// <summary>
        /// Участвовать в событии
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Post("/Events/CheckIn/{id}")]
        Task CheckIn(long id);

        /// <summary>
        /// Выйти из участия в событии
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Post("/Events/CheckOut/{id}")]
        Task CheckOut(long id);

        #endregion
    }
}
