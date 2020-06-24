using Eventador.APP.V2.Models;
using Eventador.APP.V2.Services;
using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Eventador.APP.V2.ViewModels
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        private IEventadorApi _eventadorApi;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand Command { get; set; }
        public UserModel UserModel { get; set; }


        public ProfileViewModel()
        {
            _eventadorApi = EventadorApi.ResolveApi();
            GetUserInfo();

            Command = new Command(TestCommand);
        }

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void TestCommand()
        {
            UserModel.Id++;
            OnPropertyChanged();
        }

        private void GetUserInfo()
        {
            UserModel = _eventadorApi.GetUserByToken().Result;
            UserModel.Rating = new Random().NextDouble() * 5f;
            UserModel.Info = "Info text";
        }

    }
}