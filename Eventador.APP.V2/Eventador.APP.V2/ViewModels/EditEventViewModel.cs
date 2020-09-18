using Eventador.APP.V2.Models;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Eventador.APP.V2.ViewModels
{
    public class EditEventViewModel : BaseViewModel
    {
        public EditEventModel EditEventModel { get; set; }

        public ICommand SaveCommand => new Command(async () => await Save());
        public ICommand CancelCommand => new Command(async () => await Cancel());

        public EditEventViewModel()
        {
            EditEventModel = new EditEventModel();
        }

        private async Task Save()
        {
            var smallModel = new SmallEventModel
            {
                Id = EditEventModel.Id,
                Title = EditEventModel.Title,
                Description = EditEventModel.Description,
                StartDate = EditEventModel.StartDate + EditEventModel.SelectedTime,
                SelectedAccessType = EditEventModel.SelectedAccessType,
                SelectedEventType = EditEventModel.SelectedEventType
            };

            MessagingCenter.Send(this, "UpdateEvent", smallModel);
            await NavigateBack();
        }

        private async Task Cancel()
        {
            await NavigateBack();
        }
    }
}