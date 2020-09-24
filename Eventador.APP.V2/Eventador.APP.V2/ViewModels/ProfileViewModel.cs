using Eventador.APP.V2.Common.Defines;
using Eventador.APP.V2.Models;
using Eventador.APP.V2.Services;
using System;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace Eventador.APP.V2.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        private IEventadorApi _eventadorApi;
        private IAuthService _authService;

        public ICommand LogoutCommand => new Command(() => Logout());
        public UserModel UserModel { get; set; }

        public ProfileViewModel()
        {
            _eventadorApi = EventadorApi.ResolveApi();
            _authService = DependencyService.Resolve<AuthService>();
            GetUserInfo();
        }

        private void Logout()
        {
            _authService.DeleteCredentials();
            NavigateTo(Pages.Login, null, NavigationMode.RootPage);
        }

        private void GetUserInfo()
        {
            try
            {
                UserModel = _eventadorApi.GetUserByToken().Result;
            }
            catch (Exception)
            {
                Debug.WriteLine("GetUserInfo Error");
            }
            
        }
    }
}