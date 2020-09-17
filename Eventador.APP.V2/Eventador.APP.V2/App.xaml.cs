using Eventador.APP.V2.Common;
using Eventador.APP.V2.Common.Defines;
using Eventador.APP.V2.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Eventador.APP.V2
{
    public partial class App : Application
    {
        private readonly IAuthService _authService;

        public App()
        {
            //Fix ios crash
            Current.MainPage = new ContentPage();

            DependencyService.Register<EventDataStore>();
            DependencyService.Register<AuthService>();

            _authService = DependencyService.Resolve<AuthService>();
        }

        protected override async void OnStart()
        {
            // Метод OnStart вызывается при запуске приложения.
            InitializeComponent();
            
            DialogService.Init(this);
            await NavigationService.Init(await GetStartPage());

            
        }

        protected override void OnSleep()
        {
            // Метод OnSleep вызывается когда приложение переводится в фоновый режим.
        }

        protected override void OnResume()
        {
            // Метод OnResume вызывается при выходе из фонового режима.
        }

        private async Task<Pages> GetStartPage()
        {
            long timeForRefreshToken = 8 * 60 * 60;

            var token = await SecureStorage.GetAsync("AccessToken");
            if (string.IsNullOrWhiteSpace(token))
            {
                return Pages.Login;
            }
            else
            {
                var expireString = await SecureStorage.GetAsync("Expires");
                if (!string.IsNullOrWhiteSpace(expireString))
                {
                    var expire = long.Parse(expireString);
                    long unixTime = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds();

                    if (expire - unixTime < timeForRefreshToken)
                    {
                        try
                        {
                            await _authService.RefreshToken();
                        }
                        catch (Exception)
                        {
                            _authService.DeleteCredentials();
                            return Pages.Login;
                        }
                    }
                }

                return Pages.Main;
            }
        }
    }
}