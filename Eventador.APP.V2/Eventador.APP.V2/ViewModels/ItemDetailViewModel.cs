using System;
using System.ComponentModel;
using System.Windows.Input;
using Eventador.APP.V2.Models;
using Eventador.APP.V2.Services;
using Xamarin.Forms;

namespace Eventador.APP.V2.ViewModels
{
    public class ItemDetailViewModel 
    {
        private IEventadorApi _eventadorApi;

        public bool isVis { get; set; }  = true;
        public string Title { get; set; }
        public EventModel Item { get; set; }


        public ItemDetailViewModel(long id)
        {
            _eventadorApi = EventadorApi.ResolveApi();

            var evnt = _eventadorApi.GetEventById(id).Result;
            Item = evnt;

            Title = $"[{Item.Id}]";
        }
    }
}
