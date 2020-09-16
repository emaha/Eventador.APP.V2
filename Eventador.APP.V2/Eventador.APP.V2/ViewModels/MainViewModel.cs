using Eventador.APP.V2.Common.Defines;
using System.Threading.Tasks;

namespace Eventador.APP.V2.ViewModels
{
    public class MainViewModel : BaseViewModel
    {

        public async Task GoTo(Pages page)
        {
            await NavigateTo(page);
        }
    }
}