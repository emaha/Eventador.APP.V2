using Eventador.APP.V2.Common.Defines;
using Eventador.APP.V2.Models;
using Eventador.APP.V2.Requests;
using Eventador.APP.V2.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Eventador.APP.V2.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginModel LoginModel { get; set; }
        private readonly IAuthService _authService;

        public ICommand GoToBrowseEventsCommand => MakeNavigateToCommand(Pages.BrowseEvents);
        public ICommand GoToRegisterCommand => MakeNavigateToCommand(Pages.Register);
        public ICommand SignInCommand => new Command(async () => await SignIn());

        public LoginViewModel()
        {
            LoginModel = new LoginModel();
            _authService = DependencyService.Resolve<AuthService>();
        }

        private async Task SignIn()
        {
            var request = new SignInRequest(LoginModel.Login, LoginModel.Password);
            try
            {
                await _authService.SignIn(request);
                await NavigateTo(Pages.Main, null, Common.Defines.NavigationMode.RootPage);
            }
            catch (Refit.ApiException ex)
            {
                await ShowAlert("Log in", $"Something went wrong", "D'OH!");
                return;
            }
        }

        
    }
}