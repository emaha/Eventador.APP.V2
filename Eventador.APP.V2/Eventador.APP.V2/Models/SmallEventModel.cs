using Eventador.APP.V2.Types;
using System;

namespace Eventador.APP.V2.Models
{
    /// <summary>
    /// Упрощенная модель событий для общего списка
    /// </summary>
    public class SmallEventModel
    {
        /// <summary>
        /// ID
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
        /// Тип доступа
        /// </summary>
        public AccessType SelectedAccessType { get; set; }

        /// <summary>
        /// Тип события
        /// </summary>
        public EventType SelectedEventType { get; set; }

        /// <summary>
        /// ID автора
        /// </summary>
        public long AuthorId { get; set; }

    }
}