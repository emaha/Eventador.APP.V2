using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Eventador.APP.V2.Services;
using Eventador.APP.V2.Views;

namespace Eventador.APP.V2
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<EventDataStore>();
            DependencyService.Register<EventadorApi>();

            MainPage = new MainPage();
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
