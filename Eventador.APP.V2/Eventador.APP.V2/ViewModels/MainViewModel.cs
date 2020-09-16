using Eventador.APP.V2.Common.Defines;
using System.Threading.Tasks;

namespace Eventador.APP.V2.ViewModels
{
    public class MainViewModel : BaseViewModel
    {

        public async Task GoTo(Pages page, NavigationMode navMode = NavigationMode.Normal)
        {
            await NavigateTo(page, null, navMode);
        }
    }
}