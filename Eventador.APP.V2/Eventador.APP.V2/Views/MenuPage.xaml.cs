using Eventador.APP.V2.Common.Defines;
using Eventador.APP.V2.Models;
using Eventador.APP.V2.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Eventador.APP.V2.Views
{
    [DesignTimeVisible(false)]
    public partial class MenuPage : ContentPage
    {
        public MenuViewModel MenuViewModel;
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<HomeMenuItem> menuItems;
        public MenuPage()
        {
            MenuViewModel = new MenuViewModel();
            InitializeComponent();

            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Page = Pages.BrowseEvents, Title="Browse Events" },
                new HomeMenuItem {Page = Pages.MyEvents, Title="My Events" },
                new HomeMenuItem {Page = Pages.Favourites, Title="Favourites" },
                new HomeMenuItem {Page = Pages.Messages, Title="Messages" },
                new HomeMenuItem {Page = Pages.Friends, Title="Friends" },
                new HomeMenuItem {Page = Pages.Profile, Title="Profile" },
                new HomeMenuItem {Page = Pages.Logout, Title="Logout" }
            };

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var page = ((HomeMenuItem)e.SelectedItem).Page;
                
                await RootPage.NavigateFromMenu(page);
            };
        }
    }
}