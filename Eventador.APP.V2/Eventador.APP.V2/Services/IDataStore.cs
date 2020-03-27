using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eventador.APP.V2.Services
{
    public interface IDataStore<T>
    {
        /// <summary>
        /// Добавление события
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<bool> AddItemAsync(T item);

        /// <summary>
        /// Обновление события 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<bool> UpdateItemAsync(T item);

        /// <summary>
        /// Удаление события
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteItemAsync(long id);

        /// <summary>
        /// Получить событие по ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetItemAsync(long id);

        /// <summary>
        /// Получить список событий
        /// </summary>
        /// <param name="forceRefresh"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
