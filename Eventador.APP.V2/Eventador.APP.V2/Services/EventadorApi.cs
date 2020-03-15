using Eventador.APP.V2.Common;
using Refit;

namespace Eventador.APP.V2.Services
{
    public class EventadorApi
    {
        private IEventadorApi eventadorApi;

        public EventadorApi()
        {
            eventadorApi = RestService.For<IEventadorApi>(Constants.EventadorApiURL);
        }
    }
}
