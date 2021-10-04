namespace StokedSports.Mobile.Core.Constants
{
    public class ApiConstants
    {
        //this is public https address generated using https://ngrok.com/
        //after downloading ngrok, command e.g. ngrok http https://localhost:<PORT> -host-header="localhost:<PORT>"
        //e.g: ngrok http https://localhost:44376 -host-header="localhost:8080"
        public const string BaseApiUrl = "https://6dc0-2601-681-4f00-ff30-7d5a-e19e-e00-10d0.ngrok.io/";
        public const string AuthenticateEndpoint = "api/identity/getToken";
        public const string RegisterEndpoint = "api/identity/create";
        //For googleauth
        public const string GoogleAuthEndPointUri = BaseApiUrl + "mobileauth/";
        public const string CALLBACK_SCHEME = "xamarinessentials";
    }
}
