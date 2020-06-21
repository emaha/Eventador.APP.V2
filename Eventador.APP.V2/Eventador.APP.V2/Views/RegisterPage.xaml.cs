using Eventador.APP.V2.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Eventador.APP.V2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
            BindingContext = new RegisterViewModel();
            MessagingCenter.Subscribe<RegisterViewModel>(this, "SignUpSuccess", async (sender) => 
            {
                await DisplayAlert("Registration", "Success", "Ok");
                await Navigation.PopModalAsync();
            });

            MessagingCenter.Subscribe<RegisterViewModel>(this, "SignUpError", async (sender) =>
            {
                await DisplayAlert("Registration", "Login already exist", "Ok");
            });
        }
    }
}