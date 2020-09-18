namespace Eventador.APP.V2.Common.Defines
{
    public enum Pages
    {
        CreateEvent,
        Main,
        Menu,
        Login,
        Friends,
        BrowseEvents,
        Register,
        MyEvents,
        Messages,
        Chat,
        Profile,
        Logout,
        Favourites,
        EditEvent
    }

    public enum NavigationMode
    {
        Normal,
        Modal,
        RootPage,
        Custom,
        Master
    }

    public enum PageState
    {
        Clean,
        Loading,
        Normal,
        NoData,
        Error,
        NoInternet
    }
}