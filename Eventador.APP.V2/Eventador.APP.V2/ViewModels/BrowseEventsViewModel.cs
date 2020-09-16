using Eventador.APP.V2.Common.Defines;
using Eventador.APP.V2.Models;
using Eventador.APP.V2.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Eventador.APP.V2.ViewModels
{
    public class BrowseEventsViewModel : BaseViewModel
    {
        public IDataStore<SmallEventModel> DataStore => DependencyService.Get<IDataStore<SmallEventModel>>();
        public ObservableCollection<SmallEventModel> Items { get; set; }

        public ICommand LoadItemsCommand => new Command(async () => await ExecuteLoadItemsCommand());
        public ICommand SearchItemsCommand => new Command(async () => await ExecuteSearchItemsCommand(SearchText));
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
        }

        private async Task ExecuteLoadItemsCommand()
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
            catch (Exception)
            {
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task ExecuteSearchItemsCommand(string searchText)
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