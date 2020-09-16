using Eventador.APP.V2.Models;

namespace Eventador.APP.V2.Requests
{
    /// <summary>
    /// Запрос для регистрации
    /// </summary>
    public class SignUpRequest
    {
        /// <summary>
        /// ctor
        /// </summary>
        public SignUpRequest(RegisterModel registerModel)
        {
            Login = registerModel.Login;
            Password = registerModel.Password;
            Lastname = registerModel.Lastname;
            Firstname = registerModel.Firstname;
            Email = registerModel.Email;
        }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string Lastname { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Firstname { get; set; }

        /// <summary>
        /// Электронная почта
        /// </summary>
        public string Email { get; set; }
    }
}