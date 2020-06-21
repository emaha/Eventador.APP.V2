using Eventador.APP.V2.ViewModels;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Eventador.APP.V2.Views
{
    [DesignTimeVisible(false)]
    public partial class EventDetailsPage : ContentPage
    {
        public  EventDetailsViewModel viewModel;

        public EventDetailsPage()
        {
            InitializeComponent();

            BindingContext = viewModel;
        }

        public EventDetailsPage(EventDetailsViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        private void GetInButton_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("TitleString", "Getting in", "Ok");

        }

        private void ChatButton_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("TitleString", "Chat", "Ok");

        }

        private void FinishButton_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("TitleString", "Finish", "Ok");
        }

        private async void EditButton_Clicked(object sender, EventArgs e)
        {
            var model = viewModel.Item;

            var editEventViewModel = new EditEventViewModel
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                StartDate = model.StartDate,
                AccessType = model.AccessType,
                SelectedEventType = model.Type,
                SelectedAccessType = model.AccessType,
                Type = model.Type,
                Price = model.Price
            };
            await Navigation.PushModalAsync(new NavigationPage(new EditEventPage(editEventViewModel)));
        }
    }
}