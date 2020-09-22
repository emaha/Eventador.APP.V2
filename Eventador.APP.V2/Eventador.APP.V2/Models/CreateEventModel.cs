using Eventador.APP.V2.Types;
using System;
using System.Collections.Generic;

namespace Eventador.APP.V2.Models
{
    /// <summary>
    /// Модель для создания события
    /// </summary>
    public class CreateEventModel
    {
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
        /// Список типов доступа
        /// </summary>
        public List<AccessType> AccessTypes { get; set; }

        /// <summary>
        /// Выбранный тип доступа
        /// </summary>
        public AccessType SelectedAccessType { get; set; }

        /// <summary>
        /// Список типов событий
        /// </summary>
        public List<EventType> EventTypes { get; set; }

        /// <summary>
        /// Выбранный тип события // TODO: в будущем предусмотреть множественный выбор
        /// </summary>
        public EventType SelectedEventType { get; set; }

        /// <summary>
        /// Цена участия
        /// </summary>
        public decimal Price { get; set; }
    }
}