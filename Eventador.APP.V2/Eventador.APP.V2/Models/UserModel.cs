namespace Eventador.APP.V2.Models
{

    /// <summary>
    /// Модель ответа для сущности пользователя
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// 
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string Lastname { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Firstname { get; set; }

        /// <summary>
        /// Отчетство
        /// </summary>
        public string Middlename { get; set; }

        /// <summary>
        /// Контактный телефон
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Полное имя
        /// </summary>
        public string FullName => $"{Lastname} {Firstname} {Middlename}";

        /// <summary>
        /// Рейтинг
        /// </summary>
        public double Rating { get; set; }

        /// <summary>
        /// Информация
        /// </summary>
        public string Info { get; set; }
    }
}