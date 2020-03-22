using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Eventador.APP.V2.Services;
using Eventador.APP.V2.Views;
using Xamarin.Essentials;

namespace Eventador.APP.V2
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<EventDataStore>();
            DependencyService.Register<EventadorApi>();

            CheckToken();
        }

        private async void CheckToken()
        {
            var token = await SecureStorage.GetAsync("AccessToken");
            if(!string.IsNullOrWhiteSpace(token))
            {
                MainPage = new MainPage();
            }
            else
            {
                MainPage = new LoginPage();
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
