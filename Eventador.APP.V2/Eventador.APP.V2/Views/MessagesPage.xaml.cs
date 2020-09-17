using Eventador.APP.V2.ViewModels;
using Xamarin.Forms.Xaml;

namespace Eventador.APP.V2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MessagesPage : BasePage
    {
        public MessagesPage()
        {
            BindingContext = new MessagesViewModel();
            InitializeComponent();
        }
    }
}