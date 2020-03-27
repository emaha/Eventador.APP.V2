using Eventador.APP.V2.Models;
using Eventador.APP.V2.Services;
using Eventador.APP.V2.Views;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Eventador.APP.V2.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<SmallEventModel> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        private IAuthService _authService;

        public ItemsViewModel()
        {
            _authService = DependencyService.Resolve<AuthService>();

            Title = "Browse";
            Items = new ObservableCollection<SmallEventModel>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<CreateEventPage, SmallEventModel>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as SmallEventModel;
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
            catch (Exception e)
            {

                _authService.DeleteCredentials();
                Application.Current.MainPage = new LoginPage();

                //try
                //{
                //    _authService.RefreshToken();
                //}
                //catch (Exception)
                //{
                //    _authService.DeleteCredentials();
                //    Application.Current.MainPage = new LoginPage();
                //}
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}