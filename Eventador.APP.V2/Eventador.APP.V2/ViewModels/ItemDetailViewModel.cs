using System;

using Eventador.APP.V2.Models;

namespace Eventador.APP.V2.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public bool isVis { get; set; }  = false;


        public SmallEventResponseModel Item { get; set; }
        public ItemDetailViewModel(SmallEventResponseModel item = null)
        {
            Title = item?.Title;
            Item = item;
        }
    }
}
