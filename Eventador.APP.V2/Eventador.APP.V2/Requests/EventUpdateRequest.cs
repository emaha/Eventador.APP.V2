using Eventador.APP.V2.Models;
using Eventador.APP.V2.Types;
using System;

namespace Eventador.APP.V2.Requests
{
    /// <summary>
    /// Запрос на обновление события
    /// </summary>
    public class EventUpdateRequest
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public EventType EventType { get; set; }
        public AccessType AccessType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int RegionId { get; set; }
        public decimal Price { get; set; }
        public float? Lat { get; set; }
        public float? Lon { get; set; }

        public static EventUpdateRequest Create(SmallEventModel model)
        {
            return new EventUpdateRequest
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                AccessType = model.SelectedAccessType,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                EventType = model.SelectedEventType,
                Price = model.Price
            };
        }
    }
}