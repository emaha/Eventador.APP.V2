using Eventador.APP.V2.Common.Defines;
using Xamarin.Forms;

namespace Eventador.APP.V2.Controls
{
    // Хз зачем вообще придумал этот Энтри
    public class ColoredEntry : Entry
    {
        public ColoredEntry()
        {
            BackgroundColor = Colors.EntryBackgroundColor;
            TextColor = Colors.EntryTextColor;
        }
    }
}