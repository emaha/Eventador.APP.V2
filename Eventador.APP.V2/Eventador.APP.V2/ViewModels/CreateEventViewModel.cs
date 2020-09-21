using Eventador.APP.V2.Controls;
using Eventador.APP.V2.Models;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Eventador.APP.V2.ViewModels
{
    /// <summary>
    /// Модель-представление для создания события
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class CreateEventViewModel : BaseViewModel
    {
        public CreateEventModel Model = new CreateEventModel();
        public ICommand CreateEventCommand => new Command(() => CreateEvent());
        public ICommand CancelCommand => new Command(() => Cancel());

        public CreateEventViewModel()
        {
        }

        private void CreateEvent()
        {
            var item = new SmallEventModel
            {
                Title = Model.Title,
                Description = Model.Description,
                StartDate = Model.StartDate,
                SelectedAccessType = Model.SelectedAccessType,
                SelectedEventType = Model.SelectedEventType
            };

            MessagingCenter.Send(this, "CreateEvent", item);

            // Затираем набранные данные
            Model = new CreateEventModel();
        }

        private void Cancel()
        {
            MessagingCenter.Send(this, "CancelCreateEvent");
        }

    }
}