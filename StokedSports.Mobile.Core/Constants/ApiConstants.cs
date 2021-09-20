namespace StokedSports.Mobile.Core.Constants
{
    public class ApiConstants
    {
        //this is public https address generated using https://ngrok.com/
        //after downloading ngrok, command e.g. ngrok http https://localhost:<PORT> -host-header="localhost:<PORT>"
        public const string BaseApiUrl = "https://60ba-2601-681-4f00-ff30-a4ab-c769-9881-c960.ngrok.io/";
        public const string AuthenticateEndpoint = "api/identity/getToken";
    }
}
