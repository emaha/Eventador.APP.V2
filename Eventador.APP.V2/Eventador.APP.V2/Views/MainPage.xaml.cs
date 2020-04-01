using Eventador.APP.V2.Models;
using Eventador.APP.V2.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Eventador.APP.V2.Views
{
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        private readonly IEventadorApi _eventadorApi;
        private IAuthService _authService;

        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public MainPage()
        {
            _authService = DependencyService.Resolve<AuthService>();
            _eventadorApi = EventadorApi.ResolveApi();

            RequestUserData();

            InitializeComponent();
            MasterBehavior = MasterBehavior.Popover;

            MenuPages.Add((int)MenuItemType.BrowseEvents, (NavigationPage)Detail);
        }

        /// <summary>
        /// Получние юанных о текущем пользователе
        /// </summary>
        private async void RequestUserData()
        {
            // TODO: заменить, вероятно не самая быстрая реализация
            var userId = await SecureStorage.GetAsync("UserId");
            if (string.IsNullOrWhiteSpace(userId))
            {
                var userData = await _eventadorApi.GetUserByToken();
                await SecureStorage.SetAsync("UserId", userData.Id.ToString());
            }
            
        }

        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.BrowseEvents:
                        MenuPages.Add(id, new NavigationPage(new ItemsPage()));
                        break;
                    case (int)MenuItemType.MyEvents:
                        MenuPages.Add(id, new NavigationPage(new AboutPage()));
                        break;
                    case (int)MenuItemType.Favourites:
                        MenuPages.Add(id, new NavigationPage(new FavouritesPage()));
                        break;
                    case (int)MenuItemType.Friends:
                        MenuPages.Add(id, new NavigationPage(new FriendsPage()));
                        break;
                    case (int)MenuItemType.Profile:
                        MenuPages.Add(id, new NavigationPage(new ProfilePage()));
                        break;
                    case (int)MenuItemType.Logout:
                        MenuPages.Add(id, new NavigationPage(new LoginPage()));
                        _authService.DeleteCredentials();
                        Application.Current.MainPage = new LoginPage();
                        break;
                    default:
                        return;
                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                //await Navigation.PushModalAsync(newPage);
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }
    }
}