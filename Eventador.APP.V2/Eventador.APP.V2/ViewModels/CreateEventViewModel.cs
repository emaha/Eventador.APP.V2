using Eventador.APP.V2.Types;
using System;
using System.Collections.Generic;

namespace Eventador.APP.V2.ViewModels
{
    /// <summary>
    /// Модель-представление для создания события 
    /// </summary>
    public class CreateEventViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }


        /// <summary>
        /// Дата начала
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Выбранное время
        /// </summary>
        public TimeSpan SelectedTime { get; set; } = new TimeSpan(12, 10, 20);

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