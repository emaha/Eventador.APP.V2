using Eventador.APP.V2.Models;
using Eventador.APP.V2.ViewModels;
using Xamarin.Forms;

namespace Eventador.APP.V2.Views
{
    public partial class BrowseEventsPage : BasePage
    {
        private readonly BrowseEventsViewModel viewModel;

        public BrowseEventsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new BrowseEventsViewModel();
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as SmallEventModel;
            if (item == null) return;

            await Navigation.PushAsync(new EventDetailsPage(new EventDetailsViewModel(item.Id)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.LoadItemsCommand.Execute(null);
        }

    }
}