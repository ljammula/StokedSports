using System.Threading.Tasks;
using StokedSports.Mobile.Core.Models;

namespace StokedSports.Mobile.Core.Services.General
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> Authenticate(string userName, string password);

        bool IsUserAuthenticated();
    }
}
