using Eventador.APP.V2.Models;
using Eventador.APP.V2.ViewModels;
using Xamarin.Forms;

namespace Eventador.APP.V2.Views
{
    public partial class BrowseEventsPage : BasePage
    {
        private readonly BrowseEventsViewModel viewModel;
        private readonly string Title = "Events";


        public BrowseEventsPage()
        {
            BindingContext = viewModel = new BrowseEventsViewModel();
            InitializeComponent();
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as SmallEventModel;
            if (item == null) return;

            var detailsModel = new EventDetailsViewModel(item.Id);
            var page = new NavigationPage(new EventDetailsPage(detailsModel));

            await Navigation.PushAsync(page);

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if(viewModel.Items.Count == 0)
            {
                viewModel.LoadItemsCommand.Execute(null);
            }
        }

    }
}