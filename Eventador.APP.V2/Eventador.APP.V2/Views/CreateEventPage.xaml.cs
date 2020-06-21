using Eventador.APP.V2.Models;
using Eventador.APP.V2.ViewModels;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Eventador.APP.V2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateEventPage : ContentPage
    {
        public CreateEventPage()
        {
            InitializeComponent();

            Item = new CreateEventViewModel();
            Item.AccessTypes = new List<Types.AccessType>();
            foreach (Types.AccessType item in Enum.GetValues(typeof(Types.AccessType)))
            {
                Item.AccessTypes.Add(item);
            }

            Item.EventTypes = new List<Types.EventType>();
            foreach (Types.EventType item in Enum.GetValues(typeof(Types.EventType)))
            {
                Item.EventTypes.Add(item);
            }

            BindingContext = this;
        }

        public CreateEventViewModel Item { get; set; }

        async void Save_Clicked(object sender, EventArgs e)
        {
            var smallModel = new SmallEventModel
            {
                Title = Item.Title,
                Description = Item.Description,
                StartDate = Item.StartDate + Item.SelectedTime,
                SelectedAccessType = Item.SelectedAccessType,
                SelectedEventType = Item.SelectedEventType
            };

            MessagingCenter.Send(this, "AddEvent", smallModel);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void datePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            if (Item != null)
            {
                Item.StartDate = e.NewDate;
            }
        }

        private void eventTypePicker_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void accessTypePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Item != null)
            {
                // Item.AccessType = picker.
            }
            header.Text = $"Выберите тип доступа: ({accessTypePicker.SelectedIndex}) - {accessTypePicker.Items[accessTypePicker.SelectedIndex]} ";
        }

        private void TimePicker_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
        }
    }
}