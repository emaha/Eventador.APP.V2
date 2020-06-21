using Eventador.APP.V2.Models;
using Eventador.APP.V2.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace Eventador.APP.V2.Views
{
    [DesignTimeVisible(false)]
    public partial class BrowseEventsPage : ContentPage
    {
        private BrowseEventsViewModel viewModel;

        public BrowseEventsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new BrowseEventsViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as SmallEventModel;
            if (item == null)
                return;

            await Navigation.PushAsync(new EventDetailsPage(new EventDetailsViewModel(item.Id)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new CreateEventPage()));
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
            {
                viewModel.LoadItemsCommand.Execute(null);
            }
        }
    }
}