using Eventador.APP.V2.Models;

namespace Eventador.APP.V2.ViewModels
{
    public class EditEventViewModel : BaseViewModel
    {
        public EditEventModel EditEventModel { get; set; }

        public EditEventViewModel()
        {
            EditEventModel = new EditEventModel();
        }
    }
}