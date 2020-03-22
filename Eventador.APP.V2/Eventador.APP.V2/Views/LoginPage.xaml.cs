using Eventador.APP.V2.Common;
using Eventador.APP.V2.Requests;
using Eventador.APP.V2.ResponseModels;
using Eventador.APP.V2.Services;
using Refit;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Eventador.APP.V2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {

        private IEventadorApi _eventadorApi;

        public LoginPage()
        {
            InitializeComponent();

            _eventadorApi = RestService.For<IEventadorApi>(Constants.EventadorApiURL);

            LogInButton.Clicked += LogInButton_Click;

        }

        private async void LogInButton_Click(object sender, EventArgs e)
        {
            var request = new CredentialsRequest(Email.Text, Password.Text);
            TokenModel token;
            try
            {
                token = await _eventadorApi.SignIn(request);
                await SecureStorage.SetAsync("AccessToken", token.AccessToken);
                await SecureStorage.SetAsync("RefreshToken", token.RefreshToken);
                await SecureStorage.SetAsync("Expires", token.Expires.ToString());

                Application.Current.MainPage = new MainPage();
            }
            catch (Exception)
            {
                await DisplayAlert("Log In", "Something went wrong", "Ok");
            }
        }
    }
}