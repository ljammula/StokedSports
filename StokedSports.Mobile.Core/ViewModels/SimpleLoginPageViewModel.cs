using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using StokedSports.Mobile.Core.Constants;
using StokedSports.Mobile.Core.Models;
using StokedSports.Mobile.Core.Services.General;
using StokedSports.Mobile.Core.Validators;
using StokedSports.Mobile.Core.Validators.Rules;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace StokedSports.Mobile.Core.ViewModels
{
    /// <summary>
    /// ViewModel for login page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class SimpleLoginPageViewModel : LoginViewModel
    {
        #region Fields

        private ValidatableObject<string> password;
        private readonly IAuthenticationService _authenticationService;
        private readonly ISettingsService _settingsService;
        private string _authToken;

        private readonly JsonSerializer _serializer = new JsonSerializer();

        #endregion

        #region Constructor

        public SimpleLoginPageViewModel(IAuthenticationService authenticationService, ISettingsService settingsService)
        {
            _authenticationService = authenticationService;
            _settingsService = settingsService;
            this.InitializeProperties();
            this.AddValidationRules();
            this.LoginCommand = new Command(this.LoginClicked);
            this.SignUpCommand = new Command(this.SignUpClicked);
            this.ForgotPasswordCommand = new Command(this.ForgotPasswordClicked);
            this.SocialMediaLoginCommand = new Command(this.SocialLoggedIn);
        }

        #endregion

        #region property

        /// <summary>
        /// Gets or sets the property that is bound with an entry that gets the password from user in the login page.
        /// </summary>
        public ValidatableObject<string> Password
        {
            get
            {
                return this.password;
            }

            set
            {
                if (this.password == value)
                {
                    return;
                }

                this.SetProperty(ref this.password, value);
            }
        }

        public string AuthToken
        {
            get => _authToken;
            set
            {
                if (value == _authToken)
                {
                    return;
                }
                this.SetProperty(ref this._authToken, value);
            }
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that is executed when the Log In button is clicked.
        /// </summary>
        public Command LoginCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Sign Up button is clicked.
        /// </summary>
        public Command SignUpCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Forgot Password button is clicked.
        /// </summary>
        public Command ForgotPasswordCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the social media login button is clicked.
        /// </summary>
        public Command SocialMediaLoginCommand { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// Check the password is null or empty
        /// </summary>
        /// <returns>Returns the fields are valid or not</returns>
        public bool AreFieldsValid()
        {
            bool isEmailValid = this.Email.Validate();
            bool isPasswordValid = this.Password.Validate();
            return isEmailValid && isPasswordValid;
        }

        /// <summary>
        /// Initializing the properties.
        /// </summary>
        private void InitializeProperties()
        {
            this.Password = new ValidatableObject<string>();
        }

        /// <summary>
        /// Validation rule for password
        /// </summary>
        private void AddValidationRules()
        {
            this.Password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Password Required" });
        }

        /// <summary>
        /// Invoked when the Log In button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void LoginClicked(object obj)
        {
            if (this.AreFieldsValid())
            {
                try
                {
                    //Display ActivityIndicator while processing
                    IsBusy = true;

                    // Call Api for authentication
                    var authenticationResponse = await _authenticationService.Authenticate(Email.Value, Password.Value);

                    if (authenticationResponse.IsAuthenticated)
                    {
                        // we store the Id to know if the user is already logged in to the application
                        _settingsService.UserIdSetting = authenticationResponse.User.Id;
                        _settingsService.UserNameSetting = authenticationResponse.User.FirstName;
                        await Shell.Current.DisplayAlert("Welcome back!", "Message", "OK");
                        // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
                        await Shell.Current.GoToAsync("//About");
                    }
                }
                catch (Exception e) //TODO: Move this to global exception handling
                {
                    var message = $"Error occurred during Login: {e.Message} :: {e}";
                    Debug.Write(message);
                    await Shell.Current.DisplayAlert("Oops an error occurred during login, please try again!", "Message", "OK");
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }

        /// <summary>
        /// Invoked when the Sign Up button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void SignUpClicked(object obj)
        {
            await Shell.Current.GoToAsync("//SignUpPage");
        }

        /// <summary>
        /// Invoked when the Forgot Password button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void ForgotPasswordClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when social media login button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void SocialLoggedIn(object obj)
        {
            string scheme = "Google";

            try
            {
                WebAuthenticatorResult r = null;

                if (scheme.Equals("Google"))
                {
                    var authUrl = new Uri(ApiConstants.GoogleAuthEndPointUri + scheme);
                    var callbackUrl = new Uri("xamarinessentials://");

                    r = await WebAuthenticator.AuthenticateAsync(authUrl, callbackUrl);
                }

                AuthToken = r?.AccessToken ?? r?.IdToken;
                GetUserInfoUsingToken(AuthToken);

                await Shell.Current.DisplayAlert("Welcome back!", "Message", "OK");
                // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
                await Shell.Current.GoToAsync("//About");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception in google Auth: ", ex);
                AuthToken = string.Empty;
                await Shell.Current.DisplayAlert("Oops an error occurred during login, please try again!", "Message", "OK");
            }
        }

        private async void GetUserInfoUsingToken(string authToken)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://www.googleapis.com/oauth2/v3/");
            var httpResponseMessage = await httpClient.GetAsync("tokeninfo?access_token=" + authToken);
            using (var stream = await httpResponseMessage.Content.ReadAsStreamAsync())
            using (var reader = new StreamReader(stream))
            using (var json = new JsonTextReader(reader))
            {
                var jsoncontent = _serializer.Deserialize<GoogleAuthClass>(json);
                Preferences.Set("UserToken", authToken);
                //TODO: use this email else where
                Debug.WriteLine($"Logged in email: {jsoncontent.email}");
            }
        }
        #endregion

    }
}