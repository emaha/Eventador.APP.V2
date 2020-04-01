using Eventador.APP.V2.Services;
using Eventador.APP.V2.ViewModels;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Eventador.APP.V2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();

            BindingContext = new ProfileViewModel();
        }

        private void ToolbarEdit_Clicked(object sender, System.EventArgs e)
        {

        }
    }
}