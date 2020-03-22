using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Eventador.APP.V2.Models;
using Eventador.APP.V2.ViewModels;
using System.Windows.Input;

namespace Eventador.APP.V2.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemDetailPage : ContentPage
    {
        private ItemDetailViewModel viewModel;

        public ItemDetailPage()
        {
            InitializeComponent();


            var item = new SmallEventResponseModel
            {
                Title = "Item 1",
                Description = "This is an item description."
            };

            Image img = new Image()
            {
                Source = "event2.jpg"
            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }

        public ItemDetailPage(ItemDetailViewModel viewModel)
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
    }
}