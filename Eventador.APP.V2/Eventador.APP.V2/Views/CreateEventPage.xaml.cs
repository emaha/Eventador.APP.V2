using Eventador.APP.V2.Controls;
using Eventador.APP.V2.ViewModels;
using Xamarin.Forms.Xaml;

namespace Eventador.APP.V2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateEventPage : BasePage
    {
        public CreateEventViewModel ViewModel { get; set; }

        public CreateEventPage()
        {
            InitializeComponent();
            BindingContext = ViewModel = new CreateEventViewModel();
        }
    }
}