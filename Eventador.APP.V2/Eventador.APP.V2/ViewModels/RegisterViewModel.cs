using Eventador.APP.V2.Requests;
using Eventador.APP.V2.Services;
using Eventador.APP.V2.Views;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Eventador.APP.V2.ViewModels
{
    public class RegisterViewModel
    {
        private IEventadorApi _eventadorApi;
        public ICommand SignUpUserCommand => new Command(async () => await SignUp());

        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }


        public RegisterViewModel()
        {
            _eventadorApi = EventadorApi.ResolveApi();

        }

        /// <summary>
        /// Регистрация
        /// </summary>
        /// <returns></returns>
        private async Task SignUp()
        {
            var request = new CredentialsRequest(Username, Password);
            await _eventadorApi.SignUp(request);

            MessagingCenter.Send<RegisterViewModel>(this, "SignUp");
        }
    }
}
