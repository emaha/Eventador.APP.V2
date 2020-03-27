using Eventador.APP.V2.Models;
using Eventador.APP.V2.Requests;
using Xamarin.Essentials;

namespace Eventador.APP.V2.Services
{
    public class AuthService : IAuthService
    {
        private readonly IEventadorApi _eventadorApi;

        public AuthService()
        {
            _eventadorApi = EventadorApi.ResolveApi();
        }

        public async void RefreshToken()
        {
            var request = new RefreshTokenRequest
            {
                RefreshToken = await SecureStorage.GetAsync("RefreshToken")
            };

            var token = await _eventadorApi.RefreshToken(request);
            SaveTokenToStorage(token);
        }

        private async void SaveTokenToStorage(TokenModel token)
        {
            // TODO: у IOS  тут проблемы
            await SecureStorage.SetAsync("AccessToken", token.AccessToken);
            await SecureStorage.SetAsync("RefreshToken", token.RefreshToken);
            await SecureStorage.SetAsync("Expires", token.Expires.ToString());

            EventadorApi.RefreshHttpClient();
        }

        public void DeleteCredentials()
        {
            SecureStorage.Remove("AccessToken");
            SecureStorage.Remove("RefreshToken");
            SecureStorage.Remove("Expires");

            EventadorApi.RefreshHttpClient();
        }

        public async void SignIn(CredentialsRequest request)
        {
            var token = await _eventadorApi.SignIn(request);

            SaveTokenToStorage(token);
        }

        public async void SignOut()
        {
            await _eventadorApi.SignOut();
        }
    }
}
