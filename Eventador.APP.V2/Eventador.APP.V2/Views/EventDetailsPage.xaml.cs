using Eventador.APP.V2.ViewModels;
using System;
using System.ComponentModel;

namespace Eventador.APP.V2.Views
{
    [DesignTimeVisible(false)]
    public partial class EventDetailsPage : BasePage
    {
        public EventDetailsPage()
        {
            InitializeComponent();
        }

        public EventDetailsPage(EventDetailsViewModel viewModel)
        {
            BindingContext = viewModel;
            InitializeComponent();
        }

        private async void EditButton_Clicked(object sender, EventArgs e)
        {
            //var model = viewModel.Item;

            //var editEventViewModel = new EditEventViewModel
            //{
            //    Id = model.Id,
            //    Title = model.Title,
            //    Description = model.Description,
            //    StartDate = model.StartDate,
            //    AccessType = model.AccessType,
            //    SelectedEventType = model.Type,
            //    SelectedAccessType = model.AccessType,
            //    Type = model.Type,
            //    Price = model.Price
            //};
            //await Navigation.PushModalAsync(new NavigationPage(new EditEventPage(editEventViewModel)));
        }
    }
}