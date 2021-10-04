namespace StokedSports.Mobile.Core.Constants
{
    public class ApiConstants
    {
        //this is public https address generated using https://ngrok.com/
        //after downloading ngrok, command e.g. ngrok http https://localhost:<PORT>
        //e.g: ngrok http https://localhost:5001
        public const string BaseApiUrl = "https://308e-2601-681-4f00-ff30-a8ba-d020-7852-736c.ngrok.io/";
        //Note: Make sure to add the above public URL (https://308e-2601-681-4f00-ff30-a8ba-d020-7852-736c.ngrok.io/signin-google) in https://console.cloud.google.com/apis/credentials
        public const string AuthenticateEndpoint = "api/identity/getToken";
        public const string RegisterEndpoint = "api/identity/create";
        //For googleauth
        public const string GoogleAuthEndPointUri = BaseApiUrl + "mobileauth/";
        public const string CALLBACK_SCHEME = "xamarinessentials";
    }
}
