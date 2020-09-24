using Eventador.APP.V2.Models;
using System;
using System.Collections.Generic;
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
        public ICommand CreateEventCommand => new Command(() => CreateEvent());
        public ICommand CancelCommand => new Command(() => Cancel());

        public CreateEventViewModel()
        {
            Model = new CreateEventModel();
            FillTypes();
        }

        private void FillTypes()
        {
            Model.AccessTypes = new List<Types.AccessType>();
            foreach (Types.AccessType item in Enum.GetValues(typeof(Types.AccessType)))
            {
                Model.AccessTypes.Add(item);
            }

            Model.EventTypes = new List<Types.EventType>();
            foreach (Types.EventType item in Enum.GetValues(typeof(Types.EventType)))
            {
                Model.EventTypes.Add(item);
            }
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

            ChangeTabPage(0);
        }

        private void Cancel()
        {
            ChangeTabPage(0);
        }
    }
}