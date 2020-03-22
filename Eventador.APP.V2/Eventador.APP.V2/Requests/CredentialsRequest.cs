using System;
using System.Collections.Generic;
using System.Text;

namespace Eventador.APP.V2.Requests
{
    /// <summary>
    /// Запрос для авторизации
    /// </summary>
    public class CredentialsRequest
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public CredentialsRequest(string username, string password)
        {
            Username = username;
            Password = password;
        }

        /// <summary>
        /// Имя пользователя 
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }
    }
}
