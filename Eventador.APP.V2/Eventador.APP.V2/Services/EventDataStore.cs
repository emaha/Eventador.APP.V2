using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eventador.APP.V2.Common;
using Eventador.APP.V2.Models;
using Eventador.APP.V2.Requests;
using Refit;

namespace Eventador.APP.V2.Services
{
    public class EventDataStore : IDataStore<SmallEventResponseModel>
    {
        private readonly List<SmallEventResponseModel> items;
        private IEventadorApi _eventadorApi;

        public EventDataStore()
        {
            _eventadorApi = RestService.For<IEventadorApi>(Constants.EventadorApiURL);
            items = new List<SmallEventResponseModel>();
        }

        public async Task<bool> AddItemAsync(SmallEventResponseModel item)
        {
            var request = new EventCreateRequest() { Title = item.Title, Description = item.Description, RegionId = 1 };
            await _eventadorApi.CreateEvent(request);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(SmallEventResponseModel item)
        {
            var oldItem = items.Where((SmallEventResponseModel arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(long id)
        {
            var oldItem = items.Where((SmallEventResponseModel arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<SmallEventResponseModel> GetItemAsync(long id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<SmallEventResponseModel>> GetItemsAsync(bool forceRefresh = false)
        {
            var events = await _eventadorApi.GetEventsByRegion(1);

            return events;
        }
    }
}