using Eventador.APP.V2.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Eventador.APP.V2.ViewModels
{
    public class EditEventViewModel : BaseViewModel
    {
        public List<Types.AccessType> AccessTypes { get; set; }
        public List<Types.EventType> EventTypes { get; set; }
        public EditEventModel Model { get; set; }

        public ICommand SaveCommand => new Command(async () => await Save());
        public ICommand CancelCommand => new Command(async () => await Cancel());

        public EditEventViewModel()
        {
            Model = new EditEventModel();
        }

        private void FillTypes()
        {
            AccessTypes =  new List<Types.AccessType>();
            foreach (Types.AccessType item in Enum.GetValues(typeof(Types.AccessType)))
            {
                AccessTypes.Add(item);
            }

            EventTypes = new List<Types.EventType>();
            foreach (Types.EventType item in Enum.GetValues(typeof(Types.EventType)))
            {
                EventTypes.Add(item);
            }
        }

        private async Task Save()
        {
            var smallModel = new SmallEventModel
            {
                Id = Model.Id,
                Title = Model.Title,
                Description = Model.Description,
                StartDate = Model.StartDate,
                SelectedAccessType = Model.AccessType,
                SelectedEventType = Model.EventType
            };

            MessagingCenter.Send(this, "UpdateEvent", smallModel);
            await NavigateBack();
        }

        public override void OnSetNavigationParams(Dictionary<string, object> navigationParams)
        {
            base.OnSetNavigationParams(navigationParams);
            if(navigationParams.TryGetValue("Event", out var item))
            {
                Model = (EditEventModel)item;
                FillTypes();
            }
        }

        private async Task Cancel()
        {
            await NavigateBack();
        }
    }
}