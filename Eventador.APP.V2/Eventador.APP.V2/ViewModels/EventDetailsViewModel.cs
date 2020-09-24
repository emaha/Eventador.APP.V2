using Eventador.APP.V2.Common.Defines;
using Eventador.APP.V2.Models;
using Eventador.APP.V2.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Eventador.APP.V2.ViewModels
{
    public class EventDetailsViewModel : BaseViewModel
    {
        private IEventadorApi _eventadorApi;

        public ICommand FinishCommand => new Command(async () => await Finish());
        public ICommand EditEventCommand => new Command(async () => await EditEvent());
        public ICommand GetInCommand => new Command(async () => await GetInEvent());
        public ICommand GoToEventChatCommand => new Command(async () => await GoToEventChat());

        public bool isAuthor { get; set; }
        public EventModel Item { get; set; }

        public EventDetailsViewModel(long id)
        {
            _eventadorApi = EventadorApi.ResolveApi();
            Item = _eventadorApi.GetEventById(id).Result;
            Item.ImageUrls = new[] 
            { "event2.png", "event3.png"};

            var userId = long.Parse(SecureStorage.GetAsync("UserId").Result);
            isAuthor = Item.AuthorId == userId;
        }

        private async Task EditEvent()
        {
            var editModel = EditEventModel.GetFromEventModel(Item);
            var navParams = new Dictionary<string, object>()
            {
                { "Event", editModel }
            };
            await NavigateTo(Pages.EditEvent, navParams: navParams);
        }

        private async Task GetInEvent()
        {
            await ShowAlert("GetInEvent", $"Raised", "OK");
        }

        private async Task GoToEventChat()
        {
            await ShowAlert("GoToEventChat", $"Raised", "OK");
        }

        private async Task Finish()
        {
            await _eventadorApi.FinishEvent(Item.Id);
            Item = await _eventadorApi.GetEventById(Item.Id);

            ShowToast($"Event with id={Item.Id} is finished", true,true);
            await NavigateBack();

            MessagingCenter.Send(this, "UpdateEventList");
        } 
    }
}
