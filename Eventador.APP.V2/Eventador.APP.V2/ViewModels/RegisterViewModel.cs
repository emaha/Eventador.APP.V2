using Eventador.APP.V2.Models;
using Eventador.APP.V2.Requests;
using Eventador.APP.V2.Services;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Eventador.APP.V2.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        public RegisterModel RegisterModel { get; set; }

        private IEventadorApi _eventadorApi;
        public ICommand SignUpUserCommand => new Command(async () => await SignUp());
        
        

        public RegisterViewModel()
        {
            RegisterModel = new RegisterModel();
            _eventadorApi = EventadorApi.ResolveApi();
        }

        /// <summary>
        /// Регистрация
        /// </summary>
        /// <returns></returns>
        private async Task SignUp()
        {
            var request = new SignUpRequest(RegisterModel);
            try
            {
                await _eventadorApi.SignUp(request);
            }
            catch (Refit.ApiException)
            {
                await ShowAlert("Registration", $"Something went wrong", "D'OH!");
                return;
            }
            await ShowAlert("Registration", $"You are successfully registered", "OK");
            await NavigateBack();
            
        }
    }
}