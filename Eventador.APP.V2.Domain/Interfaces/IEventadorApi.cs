using Refit;
using System.Net.Http;
using System.Threading.Tasks;

namespace Eventador.APP.V2.Interfaces
{
    [Headers("Accept: application/json")]
    public interface IEventadorApi
    {
        #region Users
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Get("/User")]
        Task<UserResponseModel> GetUser();
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Post("/User")]
        Task<HttpResponseMessage> CreateUser();

        #endregion 

        #region Events
        /// <summary>
        /// Получить детальную модель события
        /// </summary>
        /// <returns></returns>
        [Get("/Event/{id}")]
        Task<EventResponseModel> GetEventById(long id);

        [Get("/Event/Region/{id}")]
        Task<SmallEventResponseModel[]> GetEventsByRegion(long id);
        
        [Get("/Event/Author/{id}")]
        Task<SmallEventResponseModel[]> GetEventsByAuthor(long id);
        
        [Post("/Event")]
        Task CreateEvent(EventCreateRequest request);
        
        [Put("/Event)")]
        Task UpdateEvent(EventUpdateRequest request);

        [Post("/Event/Finish/{id}")]
        Task FinishEvent(long id);

        [Post("/Event/CheckIn/{id}")]
        Task CheckIn(long id);

        [Post("/Event/CheckOut/{id}")]
        Task CheckOut(long id);

        #endregion
    }
}