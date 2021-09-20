using System;
using System.Threading.Tasks;
using StokedSports.Mobile.Core.Constants;
using StokedSports.Mobile.Core.Models;
using StokedSports.Mobile.Core.Repository;
using StokedSports.Mobile.Core.Services.General;

namespace StokedSports.Mobile.Core.Services.General
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly ISettingsService _settingsService;

        public AuthenticationService(IGenericRepository genericRepository, ISettingsService settingsService)
        {
            _settingsService = settingsService;
            _genericRepository = genericRepository;
        }

        public bool IsUserAuthenticated()
        {
            return !string.IsNullOrEmpty(_settingsService.UserIdSetting);
        }

        public async Task<AuthenticationResponse> Authenticate(string userName, string password)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.AuthenticateEndpoint
            };

            AuthenticationRequest authenticationRequest = new AuthenticationRequest()
            {
                UserName = userName,
                Password = password
            };

            return await _genericRepository.PostAsync<AuthenticationRequest, AuthenticationResponse>(builder.ToString(), authenticationRequest);
        }
    }
}
