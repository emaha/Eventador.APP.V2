using System;

using Eventador.APP.V2.Models;

namespace Eventador.APP.V2.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public SmallEventResponseModel Item { get; set; }
        public ItemDetailViewModel(SmallEventResponseModel item = null)
        {
            Title = item?.Title;
            Item = item;
        }
    }
}
