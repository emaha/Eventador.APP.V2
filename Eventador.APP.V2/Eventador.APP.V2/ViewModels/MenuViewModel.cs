using Eventador.APP.V2.Common.Defines;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Eventador.APP.V2.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        public ICommand GoToProfileCommand => MakeNavigateToCommand(Pages.Profile);


        
    }
}