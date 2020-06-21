using Eventador.APP.V2.Models;
using Eventador.APP.V2.Services;
using Eventador.APP.V2.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Eventador.APP.V2.ViewModels
{
    public class BrowseEventsViewModel : BaseViewModel
    {
        public ObservableCollection<SmallEventModel> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        private IAuthService _authService;

        public BrowseEventsViewModel()
        {
            _authService = DependencyService.Resolve<AuthService>();

            PageTitle = "Browse";
            Items = new ObservableCollection<SmallEventModel>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<CreateEventPage, SmallEventModel>(this, "AddEvent", async (obj, item) =>
            {
                var newItem = item as SmallEventModel;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });

            MessagingCenter.Subscribe<EditEventPage, SmallEventModel>(this, "UpdateEvent", async (obj, item) =>
            {
                var newItem = item as SmallEventModel;
                await DataStore.UpdateItemAsync(newItem);
                OnPropertyChanged();
            });
        }

        private async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception)
            {
                
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}