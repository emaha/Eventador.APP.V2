using Eventador.APP.V2.Requests;

namespace Eventador.APP.V2.Services
{
    public interface IAuthService
    {
        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="request"></param>
        void SignIn(CredentialsRequest request);
        
        /// <summary>
        /// Выход
        /// </summary>
        void SignOut();

        /// <summary>
        /// Обновление токена
        /// </summary>
        void RefreshToken();

        /// <summary>
        /// Удаление ключей
        /// </summary>
        void DeleteCredentials();
    }
}
