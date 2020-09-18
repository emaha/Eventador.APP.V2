using Eventador.APP.V2.Models;
using Eventador.APP.V2.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Eventador.APP.V2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditEventPage : BasePage
    {
        public EditEventViewModel Item { get; set; }

        public EditEventPage()
        {
            BindingContext = Item = new EditEventViewModel();
            InitializeComponent();
        }

        public EditEventPage(EditEventViewModel viewModel)
        {
            BindingContext = Item = viewModel;
            InitializeComponent();
        }

        

        private void datePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            if (Item != null)
            {
                Item.EditEventModel.StartDate = e.NewDate;
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