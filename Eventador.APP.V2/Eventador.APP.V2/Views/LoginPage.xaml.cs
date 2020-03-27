using Eventador.APP.V2.Models;
using Eventador.APP.V2.Requests;
using Eventador.APP.V2.Services;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Eventador.APP.V2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private IAuthService _authService;

        public LoginPage()
        {
            _authService = DependencyService.Resolve<AuthService>();
            InitializeComponent();

            LogInButton.Clicked += LogInButton_Click;
        }

        private async void LogInButton_Click(object sender, EventArgs e)
        {
            var request = new CredentialsRequest(Email.Text, Password.Text);
            try
            {
                _authService.SignIn(request);
                Application.Current.MainPage = new MainPage();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Log In", $"Something went wrong.\n{ex}", "Ok");
            }
            
        }
    }
}