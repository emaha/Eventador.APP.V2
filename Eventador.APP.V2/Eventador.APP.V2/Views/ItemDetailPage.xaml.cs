using Eventador.APP.V2.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace Eventador.APP.V2.Views
{
    [DesignTimeVisible(false)]
    public partial class ItemDetailPage : ContentPage
    {
        public  ItemDetailViewModel viewModel;

        public ItemDetailPage()
        {
            InitializeComponent();

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