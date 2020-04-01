using Eventador.APP.V2.Services;
using Eventador.APP.V2.Views;
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
            InitializeComponent();

            DependencyService.Register<EventDataStore>();
            DependencyService.Register<AuthService>();

            _authService = DependencyService.Resolve<AuthService>();

            MainPage = GetStartPage();
        }

        private Page GetStartPage()
        {
            long timeForRefreshToken = 8 * 60 * 60;

            var token = SecureStorage.GetAsync("AccessToken").Result;
            if (string.IsNullOrWhiteSpace(token))
            {
                return new LoginPage();
            }
            else
            {
                var expireString = SecureStorage.GetAsync("Expires").Result;
                if (!string.IsNullOrWhiteSpace(expireString))
                {
                    var expire = long.Parse(expireString);
                    long unixTime = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds();

                    if (expire - unixTime < timeForRefreshToken)
                    {
                        try
                        {
                            _authService.RefreshToken();
                        }
                        catch (Exception)
                        {
                            _authService.DeleteCredentials();
                            return new LoginPage();
                        }
                    }
                }

                return new MainPage();
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
