using Eventador.APP.V2.Models;
using Eventador.APP.V2.Requests;
using System.Threading.Tasks;
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

        public async Task RefreshToken()
        {
            var request = new RefreshTokenRequest
            {
                RefreshToken = await SecureStorage.GetAsync("RefreshToken")
            };

            var token = await _eventadorApi.RefreshToken(request);
            await SaveTokenToStorage(token);
        }
        
        private async Task SaveTokenToStorage(TokenModel token)
        {
            // TODO: у IOS  тут проблемы
            await SecureStorage.SetAsync("AccessToken", token.AccessToken);
            await SecureStorage.SetAsync("RefreshToken", token.RefreshToken);
            await SecureStorage.SetAsync("Expires", token.Expires.ToString());
        }

        public void DeleteCredentials()
        {
            SecureStorage.Remove("UserId");
            SecureStorage.Remove("AccessToken");
            SecureStorage.Remove("RefreshToken");
            SecureStorage.Remove("Expires");
        }

        public async Task SignIn(SignInRequest request)
        {
            var token = await _eventadorApi.SignIn(request);

            await SaveTokenToStorage(token);
        }

        public async void SignOut()
        {
            await _eventadorApi.SignOut();
        }
    }
}
