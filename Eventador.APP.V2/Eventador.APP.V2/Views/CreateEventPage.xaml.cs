using Eventador.APP.V2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            Item = new SmallEventResponseModel
            {
                Title = "Item name",
                Description = "This is an item description."
            };

            BindingContext = this;
        }

        public SmallEventResponseModel Item { get; set; }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void datePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            if(Item != null)
            {
                Item.StartDate = e.NewDate;
            }
        }

        private void picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(Item != null)
            {
                // Item.AccessType = picker.
            }
            header.Text = $"Выберите тип доступа: ({picker.SelectedIndex}) - {picker.Items[picker.SelectedIndex]} ";
        }
    }
}