using Eventador.APP.V2.Models;
using Eventador.APP.V2.Services;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Eventador.APP.V2.ViewModels
{
    public class EventDetailsViewModel : INotifyPropertyChanged
    {
        private IEventadorApi _eventadorApi;

        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand FinishCommand => new Command(async () => await Finish());

        public bool isAuthor { get; set; }
        public EventModel Item { get; set; }

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public EventDetailsViewModel(long id)
        {
            _eventadorApi = EventadorApi.ResolveApi();
            Item = _eventadorApi.GetEventById(id).Result;
            Item.ImageUrls = new[] 
            { "event2.png", "event3.png"};

            var userId = long.Parse(SecureStorage.GetAsync("UserId").Result);
            isAuthor = Item.AuthorId == userId;
        }

        private async Task Finish()
        {
            await _eventadorApi.FinishEvent(Item.Id);
            Item = await _eventadorApi.GetEventById(Item.Id);
            OnPropertyChanged("Item");
        } 
    }
}
