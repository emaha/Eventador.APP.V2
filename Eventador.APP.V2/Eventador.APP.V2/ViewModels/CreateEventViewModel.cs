using Eventador.APP.V2.Models;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Eventador.APP.V2.ViewModels
{
    /// <summary>
    /// Модель-представление для создания события
    /// </summary>
    public class CreateEventViewModel : BaseViewModel
    {
        public CreateEventModel Model { get; set; }
        public ICommand CreateEventCommand => new Command(async () => await CreateEvent());
        public ICommand CancelCommand => new Command(async () => await NavigateBack());

        public CreateEventViewModel()
        {
            Model = new CreateEventModel();
        }

        private async Task CreateEvent()
        {
            var item = new SmallEventModel
            {
                Title = Model.Title,
                Description = Model.Description,
                StartDate = Model.StartDate + Model.SelectedTime,
                SelectedAccessType = Model.SelectedAccessType,
                SelectedEventType = Model.SelectedEventType
            };

            //MessagingCenter.Send(this, "CreateEvent", item);
            _ = await NavigateBack();
        }

        
    }
}