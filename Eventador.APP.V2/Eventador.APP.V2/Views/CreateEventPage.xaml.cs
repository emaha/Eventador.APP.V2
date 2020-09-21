using Eventador.APP.V2.ViewModels;
using System;
using System.Collections.Generic;
using Xamarin.Forms.Xaml;

namespace Eventador.APP.V2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateEventPage : BasePage
    {
        public CreateEventViewModel ViewModel = new CreateEventViewModel();

        public CreateEventPage()
        {
            InitializeComponent();
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

            
        }
    }
}