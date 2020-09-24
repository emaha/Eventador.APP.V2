using Eventador.APP.V2.ViewModels;
using System.ComponentModel;
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
            DateTimePicker.PropertyChanged += DateTimePicker_PropertyChanged;
        }

        private void DateTimePicker_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DateTimePicker.DateTime))
            {
                ViewModel.Model.StartDate = DateTimePicker.DateTime;
            }
        }
    }
}