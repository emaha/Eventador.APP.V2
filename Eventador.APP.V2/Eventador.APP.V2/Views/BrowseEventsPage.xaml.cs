using Eventador.APP.V2.Models;
using Eventador.APP.V2.ViewModels;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Eventador.APP.V2.Views
{
    public partial class BrowseEventsPage : BasePage
    {
        private readonly BrowseEventsViewModel viewModel = new BrowseEventsViewModel();

        public BrowseEventsPage()
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as SmallEventModel;
            if (item == null) return;

            var detailsModel = new EventDetailsViewModel(item.Id);
            var eventDetailsPage = new EventDetailsPage(detailsModel);
            var page = new NavigationPage(eventDetailsPage);

            await Navigation.PushAsync(page);

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (viewModel.Items.Count == 0)
            {
                Task.Run(() => viewModel.LoadItemsCommand.Execute(null));
            }
        }

    }
}