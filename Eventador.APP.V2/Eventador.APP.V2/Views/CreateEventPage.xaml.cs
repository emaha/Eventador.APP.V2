using Eventador.APP.V2.Models;
using Eventador.APP.V2.ViewModels;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Eventador.APP.V2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateEventPage : BasePage
    {
        public CreateEventViewModel ViewModel { get; set; }

        public CreateEventPage()
        {
            ViewModel = new CreateEventViewModel();
            BindingContext = ViewModel;

            ViewModel.Model.AccessTypes = new List<Types.AccessType>();
            foreach (Types.AccessType item in Enum.GetValues(typeof(Types.AccessType)))
            {
                ViewModel.Model.AccessTypes.Add(item);
            }

            ViewModel.Model.EventTypes = new List<Types.EventType>();
            foreach (Types.EventType item in Enum.GetValues(typeof(Types.EventType)))
            {
                ViewModel.Model.EventTypes.Add(item);
            }

            InitializeComponent();
        }

        private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            if (ViewModel != null)
            {
                ViewModel.Model.StartDate = e.NewDate;
            }
        }

        private void EventTypePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void AccessTypePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ViewModel != null)
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