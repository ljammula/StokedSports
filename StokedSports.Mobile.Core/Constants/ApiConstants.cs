namespace StokedSports.Mobile.Core.Constants
{
    public class ApiConstants
    {
        //this is public https address generated using https://ngrok.com/
        //after downloading ngrok, command e.g. ngrok http https://localhost:<PORT> -host-header="localhost:<PORT>"
        public const string BaseApiUrl = "https://8f8a-2601-681-4f00-ff30-b0a3-1b7b-7907-93d0.ngrok.io/";
        public const string AuthenticateEndpoint = "api/identity/getToken";
        public const string RegisterEndpoint = "api/identity/create";
    }
}
