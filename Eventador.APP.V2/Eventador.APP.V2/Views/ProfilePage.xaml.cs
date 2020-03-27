using Eventador.APP.V2.Services;
using Eventador.APP.V2.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Eventador.APP.V2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        ProfileViewModel viewModel;
        private IEventadorApi _eventadorApi;

        public ProfilePage()
        {
            _eventadorApi = EventadorApi.ResolveApi();

            InitializeComponent();
            viewModel = new ProfileViewModel();

            BindingContext = viewModel;
        }
    }
}