using Eventador.APP.V2.Types;
using System;

namespace Eventador.APP.V2.Models
{
    /// <summary>
    /// Модель редактирования события
    /// </summary>
    public class EditEventModel
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
        /// Дата начала
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Выбранный тип доступа
        /// </summary>
        public AccessType AccessType { get; set; }

        /// <summary>
        /// Выбранный тип события // TODO: в будущем предусмотреть множественный выбор
        /// </summary>
        public EventType EventType { get; set; }

        /// <summary>
        /// Цена участия
        /// </summary>
        public decimal Price { get; set; }

        public static object GetFromEventModel(EventModel item)
        {
            return new EditEventModel
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                StartDate = item.StartDate,
                AccessType = item.AccessType,
                EventType = item.Type
            };
        }
    }
}