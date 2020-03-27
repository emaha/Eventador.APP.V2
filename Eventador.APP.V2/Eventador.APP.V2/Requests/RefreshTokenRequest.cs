namespace Eventador.APP.V2.Requests
{
    /// <summary>
    /// Запрос на создание Refresh-токена
    /// </summary>
    public class RefreshTokenRequest
    {
        /// <summary>
        /// Токен для обновления
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
