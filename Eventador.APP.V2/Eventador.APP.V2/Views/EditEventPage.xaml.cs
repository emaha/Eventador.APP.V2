using Acr.UserDialogs;
using Eventador.APP.V2.ViewModels;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms.Xaml;

namespace Eventador.APP.V2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditEventPage : BasePage
    {
        public EditEventViewModel ViewModel { get; set; }

        public EditEventPage()
        {
            BindingContext = ViewModel = new EditEventViewModel();
            InitializeComponent();
        }
    }
}