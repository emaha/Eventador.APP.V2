using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Eventador.APP.V2.Models;
using Eventador.APP.V2.Views;

namespace Eventador.APP.V2.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<SmallEventResponseModel> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<SmallEventResponseModel>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<CreateEventPage, SmallEventResponseModel>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as SmallEventResponseModel;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });
        }

        async Task ExecuteLoadItemsCommand()
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
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}