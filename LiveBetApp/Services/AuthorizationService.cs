using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LiveBetApp.Services
{
    public static class AuthorizationService
    {
        public static bool Check()
        {

            string getSidUrl = Common.Config.GetConfigModel().getSidUrl;
            Uri SocketIoUrl = new Uri(getSidUrl);
            string id = HttpUtility.ParseQueryString(SocketIoUrl.Query).Get("id");

            string cookie = Common.Config.GetConfigModel().cookie;

            //_UserName=QZC8B303444;

            return DataStore.Authorizations.Any(item => item.Id == id && cookie.Contains("_UserName="+item.UserName+";"));
        }
    }
}
