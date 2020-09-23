using Eventador.APP.V2.Models;
using Eventador.APP.V2.Services;
using Eventador.APP.V2.ViewModels;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Eventador.APP.V2.Views
{
    [DesignTimeVisible(false)]
    public partial class MainPage : TabbedPage
    {
        private readonly IEventadorApi _eventadorApi;
        public MainViewModel MainViewModel = new MainViewModel();

        public MainPage()
        {
            _eventadorApi = EventadorApi.ResolveApi();

            RequestUserData();
            BindingContext = MainViewModel;

            NavigationPage.SetHasNavigationBar(this, false);

            MessagingCenter.Unsubscribe<CreateEventViewModel, SmallEventModel>(this, "CreateEvent");
            MessagingCenter.Subscribe<CreateEventViewModel, SmallEventModel>(this, "CreateEvent", async (obj, _) =>
            {
                await Task.Delay(600);
                CurrentPage = Children[0];
            });

            MessagingCenter.Unsubscribe<CreateEventViewModel>(this, "CancelCreateEvent");
            MessagingCenter.Subscribe<CreateEventViewModel>(this, "CancelCreateEvent", (obj) =>
            {
                CurrentPage = Children[0];
            });

            InitializeComponent();
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
                try
                {
                    var userData = await _eventadorApi.GetUserByToken();
                    await SecureStorage.SetAsync("UserId", userData.Id.ToString());
                }
                catch(Exception)
                {
                    Debug.WriteLine("GetUserByToken Error");
                }
                
            }
        }

        
    }
}