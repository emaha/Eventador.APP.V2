using Eventador.APP.V2.Types;
using System;

namespace Eventador.APP.V2.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class EventModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Заголовок
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Заголовочная картинка
        /// </summary>
        public string TitleImageUrl { get; set; }

        /// <summary>
        /// Список картинок отображаемых в деталях
        /// </summary>
        public string[] ImageUrls { get; set; }

        /// <summary>
        /// Id категорий к которым  относится событие
        /// </summary>
        public EventType EventType { get; set; }

        /// <summary>
        /// Статус события
        /// </summary>
        public EventStatus EventStatus { get; set; }

        /// <summary>
        /// Дата начала
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Дата окончания
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Стоимость входа/билета/участия/депозит
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Регион
        /// </summary>
        public int RegionId { get; set; }

        /// <summary>
        /// Широта
        /// </summary>
        public float? Lat { get; set; }

        /// <summary>
        /// Долгота
        /// </summary>
        public float? Lon { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Id аккаунта создателя
        /// </summary>
        public long AuthorId { get; set; }


        /// <summary>
        /// Тип доступа (видимости)
        /// </summary>
        public AccessType AccessType { get; set; }

    }
}