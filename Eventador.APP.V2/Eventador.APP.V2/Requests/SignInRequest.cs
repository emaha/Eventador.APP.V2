namespace Eventador.APP.V2.Requests
{
    /// <summary>
    /// Запрос для авторизации
    /// </summary>
    public class SignInRequest
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        public SignInRequest(string login, string password)
        {
            Login = login;
            Password = password;
        }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }

    }
}