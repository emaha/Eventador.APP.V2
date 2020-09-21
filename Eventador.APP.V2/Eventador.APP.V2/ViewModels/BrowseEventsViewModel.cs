using Eventador.APP.V2.Common.Defines;
using Eventador.APP.V2.Models;
using Eventador.APP.V2.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Eventador.APP.V2.ViewModels
{
    public class BrowseEventsViewModel : BaseViewModel
    {
        public IDataStore<SmallEventModel> DataStore => DependencyService.Get<IDataStore<SmallEventModel>>();
        public ObservableCollection<SmallEventModel> Items { get; set; }

        public ICommand LoadItemsCommand => new Command(async () => await ExecuteLoadItems());
        public ICommand SearchItemsCommand => new Command(async () => await ExecuteSearchItems(SearchText));
        public ICommand GoToCreateEventPageCommand => MakeNavigateToCommand(Pages.CreateEvent);

        public string SearchText { get; set; }

        private bool isBusy = false;

        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        public BrowseEventsViewModel()
        {
            PageTitle = "Browse";
            Items = new ObservableCollection<SmallEventModel>();

            MessagingCenter.Unsubscribe<CreateEventViewModel, SmallEventModel>(this, "CreateEvent");
            MessagingCenter.Subscribe<CreateEventViewModel, SmallEventModel>(this, "CreateEvent", async (obj, item) =>
            {
                Items.Add(item);

                var id = await DataStore.AddItemAsync(item);
                await ShowAlert("Create", $"Event created successful with id = {id}", "OK");
                item.Id = id;
            });

            MessagingCenter.Unsubscribe<EditEventViewModel, SmallEventModel>(this, "UpdateEvent");
            MessagingCenter.Subscribe<EditEventViewModel, SmallEventModel>(this, "UpdateEvent", async (obj, item) =>
            {
                await DataStore.UpdateItemAsync(item);
                await ShowAlert("Update", $"Event updated successfuly", "OK");
            });
        }

        ~BrowseEventsViewModel()
        {
            MessagingCenter.Unsubscribe<CreateEventViewModel, SmallEventModel>(this, "CreateEvent");
            MessagingCenter.Unsubscribe<CreateEventViewModel, SmallEventModel>(this, "UpdateEvent");
        }

        EventDetailsViewModel _yourSelectedItem;
        public EventDetailsViewModel YourSelectedItem
        {
            get
            {
                return _yourSelectedItem;
            }
            set
            {
                _yourSelectedItem = value;
                OnPropertyChanged("YourSelectedItem");
            }
        }

        private async Task ExecuteLoadItems()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsByRegionAsync();

                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception){}
            finally
            {
                // Правильное исчезновение иконки загрузки получилось решить только так.
                if (Device.RuntimePlatform == Device.Android)
                {
                    await Task.Delay(100);
                }
                IsBusy = false;
            }
        }

        private async Task ExecuteSearchItems(string searchText)
        {
            var items = await DataStore.GetItemsBySearchRequestAsync(searchText);
            Items.Clear();
            foreach (var item in items)
            {
                Items.Add(item);
            }
        }
    }
}