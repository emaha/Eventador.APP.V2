using Eventador.APP.V2.Requests;
using System.Threading.Tasks;

namespace Eventador.APP.V2.Services
{
    public interface IAuthService
    {
        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="request"></param>
        Task SignIn(CredentialsRequest request);
        
        /// <summary>
        /// Выход
        /// </summary>
        void SignOut();

        /// <summary>
        /// Обновление токена
        /// </summary>
        Task RefreshToken();

        /// <summary>
        /// Удаление ключей
        /// </summary>
        void DeleteCredentials();
    }
}
