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
        private readonly List<SmallEventModel> _items;
        private readonly IEventadorApi _eventadorApi;

        public EventDataStore()
        {
            _eventadorApi = EventadorApi.ResolveApi();
            _items = new List<SmallEventModel>();
        }

        public async Task<long> AddItemAsync(SmallEventModel item)
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
            var id = await _eventadorApi.CreateEvent(request);

            return id;
        }

        public async Task<bool> UpdateItemAsync(SmallEventModel item)
        {
            var oldItem = _items.Where((SmallEventModel arg) => arg.Id == item.Id).FirstOrDefault();
            _items.Remove(oldItem);
            _items.Add(item);

            EventUpdateRequest request = EventUpdateRequest.Create(item);
            await _eventadorApi.UpdateEvent(request);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(long id)
        {
            var oldItem = _items.Where((SmallEventModel arg) => arg.Id == id).FirstOrDefault();
            _items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<SmallEventModel> GetItemAsync(long id)
        {
            return await Task.FromResult(_items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<SmallEventModel>> GetItemsByRegionAsync()
        {
            long regionId = 1;

            var result = await Policy
              .Handle<Refit.ApiException>()
              .Or<Refit.ValidationApiException>()
              .Retry()
              .Execute(async () =>
              {
                  return await _eventadorApi.GetEventsByRegion(regionId);
              });

            return result;
        }

        public async Task<IEnumerable<SmallEventModel>> GetItemsByAuthorAsync()
        {
            long authorId = 1;

            var result = await Policy
              .Handle<Refit.ApiException>()
              .Or<Refit.ValidationApiException>()
              .Retry()
              .Execute(async () =>
              {
                  return await _eventadorApi.GetEventsByAuthor(authorId);
              });

            return result;
        }

        public async Task<IEnumerable<SmallEventModel>> GetItemsBySearchRequestAsync(string request)
        {
            var result = await Policy
              .Handle<Refit.ApiException>()
              .Or<Refit.ValidationApiException>()
              .Retry()
              .Execute(async () =>
              {
                  return await _eventadorApi.GetEventsBySearchRequest(request);
              });

            return result;
        }
    }
}