using Eventador.APP.V2.Models;
using Eventador.APP.V2.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Eventador.APP.V2.ViewModels
{
    public class BrowseEventsViewModel : BaseViewModel
    {
        public string SearchText { get; set; }
        public ObservableCollection<SmallEventModel> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command SearchItemsCommand { get; set; }

        public BrowseEventsViewModel()
        {
            PageTitle = "Browse";
            Items = new ObservableCollection<SmallEventModel>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            SearchItemsCommand = new Command(async () => await ExecuteSearchItemsCommand(SearchText));

            MessagingCenter.Subscribe<CreateEventPage, SmallEventModel>(this, "AddEvent", async (obj, item) =>
            {
                Items.Add(item);
                var id = await DataStore.AddItemAsync(item);
                item.Id = id;
            });

            MessagingCenter.Subscribe<EditEventPage, SmallEventModel>(this, "UpdateEvent", async (obj, item) =>
            {
                await DataStore.UpdateItemAsync(item);
                OnPropertyChanged();
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