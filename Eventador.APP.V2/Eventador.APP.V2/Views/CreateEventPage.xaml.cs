using Eventador.APP.V2.Controls;
using Eventador.APP.V2.ViewModels;
using System.ComponentModel;
using Xamarin.Forms.Xaml;

namespace Eventador.APP.V2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateEventPage : BasePage
    {
        public CreateEventViewModel ViewModel { get; set; }

        public CreateEventPage()
        {
            InitializeComponent();
            BindingContext = ViewModel = new CreateEventViewModel();
            DateTimePicker.PropertyChanged += DateTimePicker_PropertyChanged;
            
        }

        ~CreateEventPage()
        {
            DateTimePicker.PropertyChanged -= DateTimePicker_PropertyChanged;
        }

        // Никак не побеждалась проблема обновления переменной, нашел только такой обход.
        private void DateTimePicker_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DateTimePicker.DateTime))
            {
                ViewModel.Model.StartDate = DateTimePicker.DateTime;
            }
        }
    }
}