using System;

namespace Eventador.APP.V2.Models
{
    /// <summary>
    /// Упрощенная модель событий лдя общего списка
    /// </summary>
    public class SmallEventResponseModel
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

    }
}