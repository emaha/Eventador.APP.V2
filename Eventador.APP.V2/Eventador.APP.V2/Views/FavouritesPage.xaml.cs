using Eventador.APP.V2.ViewModels;
using Xamarin.Forms.Xaml;

namespace Eventador.APP.V2.Views
{
    public partial class FavouritesPage : BasePage
    {
        public FavouritesPage()
        {
            BindingContext = new FavouritesViewModel();
            InitializeComponent();
        }
    }
}