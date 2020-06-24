using Eventador.APP.V2.Models;
using Eventador.APP.V2.Requests;
using Polly;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventador.APP.V2.Services
{
    public class EventDataStore : IDataStore<SmallEventModel>
    {
        private readonly List<SmallEventModel> items;
        private readonly IEventadorApi _eventadorApi;

        public EventDataStore()
        {
            _eventadorApi = EventadorApi.ResolveApi();
            items = new List<SmallEventModel>();
        }

        public async Task<bool> AddItemAsync(SmallEventModel item)
        {
            var request = new EventCreateRequest()
            {
                Title = item.Title,
                Description = item.Description,
                RegionId = 1,
                StartDate = item.StartDate,
                AccessType = item.SelectedAccessType,
                EventType = item.SelectedEventType
            };
            await _eventadorApi.CreateEvent(request);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(SmallEventModel item)
        {
            var oldItem = items.Where((SmallEventModel arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            EventUpdateRequest request = EventUpdateRequest.Create(item);
            await _eventadorApi.UpdateEvent(request);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(long id)
        {
            var oldItem = items.Where((SmallEventModel arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<SmallEventModel> GetItemAsync(long id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<SmallEventModel>> GetItemsAsync()
        {
            //return await _eventadorApi.GetEventsByRegion(1);

            var result = await Policy
              .Handle<Refit.ApiException>()
              .Or<Refit.ValidationApiException>()
              .Retry()
              .Execute(async () =>
              {
                  return await _eventadorApi.GetEventsByRegion(1);
              });

            return result;
        }
    }
}