using Eventador.APP.V2.Models;
using Eventador.APP.V2.Services;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Eventador.APP.V2.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        private IEventadorApi _eventadorApi;

        public ICommand Command => new Command(() => TestCommand());
        public UserModel UserModel { get; set; }

        public ProfileViewModel()
        {
            _eventadorApi = EventadorApi.ResolveApi();
            GetUserInfo();
        }
        private void TestCommand()
        {
            UserModel.Id++;
            OnPropertyChanged();
        }

        private void GetUserInfo()
        {
            UserModel = _eventadorApi.GetUserByToken().Result;
        }
    }
}