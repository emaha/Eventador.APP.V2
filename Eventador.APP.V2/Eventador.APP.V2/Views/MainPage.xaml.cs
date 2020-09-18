using Eventador.APP.V2.Common.Defines;
using Eventador.APP.V2.Services;
using Eventador.APP.V2.ViewModels;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Eventador.APP.V2.Views
{
    [DesignTimeVisible(false)]
    public partial class MainPage : TabbedPage
    {
        private readonly IEventadorApi _eventadorApi;
        private readonly IAuthService _authService;
        public MainViewModel MainViewModel { get; set; }

        public MainPage()
        {
            InitializeComponent();
            _eventadorApi = EventadorApi.ResolveApi();
            _authService = DependencyService.Resolve<AuthService>();

            RequestUserData();
            BindingContext = MainViewModel = new MainViewModel();

            NavigationPage.SetHasNavigationBar(this, false);

            MessagingCenter.Unsubscribe<CreateEventViewModel>(this, "CreateEvent");
            MessagingCenter.Subscribe<CreateEventViewModel>(this, "CreateEvent", (obj) =>
            {
                CurrentPage = Children[0];
            });
        }

        /// <summary>
        /// Получние данных о текущем пользователе
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

        public async Task NavigateFromMenu(Pages page)
        {
            if (page == Pages.Logout)
            {
                _authService.DeleteCredentials();
                await MainViewModel.GoTo(Pages.Login, Common.Defines.NavigationMode.RootPage);
            }
            else
            {
                await MainViewModel.GoTo(page);
            }

            if (Device.RuntimePlatform == Device.Android)
            {
                await Task.Delay(100);
            }

            //IsPresented = false;
        }
    }
}