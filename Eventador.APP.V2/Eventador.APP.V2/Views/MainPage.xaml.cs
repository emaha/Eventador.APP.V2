using Eventador.APP.V2.Common;
using Eventador.APP.V2.Common.Defines;
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

            MessagingCenter.Unsubscribe<MessageBus, int>(this, Consts.NavigationTabPageMessage);
            MessagingCenter.Subscribe<MessageBus, int>(this, Consts.NavigationTabPageMessage, NavigationTabPageCallback);

            InitializeComponent();
        }

        private void NavigationTabPageCallback(MessageBus bus, int pageNumber)
        {
            CurrentPage = Children[pageNumber];
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