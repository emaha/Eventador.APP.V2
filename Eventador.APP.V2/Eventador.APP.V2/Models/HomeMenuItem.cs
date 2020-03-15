namespace Eventador.APP.V2.Models
{
    public enum MenuItemType
    {
        BrowseEvents,
        MyEvents,
        Favourites,
        Profile,
        Friends,
        Messages
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
