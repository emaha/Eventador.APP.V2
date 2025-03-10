﻿using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Eventador.APP.V2.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            PageTitle = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://xamarin.com"));
        }

        public ICommand OpenWebCommand { get; }
    }
}