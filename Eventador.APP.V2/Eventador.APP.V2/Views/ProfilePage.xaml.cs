using Eventador.APP.V2.ViewModels;
using Xamarin.Forms.Xaml;

namespace Eventador.APP.V2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : BasePage
    {
        public ProfilePage()
        {
            BindingContext = new ProfileViewModel();
            InitializeComponent();
        }

        private void ToolbarEdit_Clicked(object sender, System.EventArgs e)
        {
        }
    }
}