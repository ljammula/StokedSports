namespace StokedSports.Mobile.Core.Constants
{
    public class ApiConstants
    {
        //this is public https address generated using https://ngrok.com/
        //after downloading ngrok, command e.g. ngrok http https://localhost:<PORT> -host-header="localhost:<PORT>"
        public const string BaseApiUrl = "https://e5ca-2601-681-4f00-ff30-1898-1366-646c-89be.ngrok.io/";
        public const string AuthenticateEndpoint = "api/identity/getToken";
        public const string RegisterEndpoint = "api/identity/create";
    }
}
