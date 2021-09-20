using StokedSports.Mobile.Core.Services.General;
using StokedSports.Mobile.Core.Validators;
using StokedSports.Mobile.Core.Validators.Rules;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace StokedSports.Mobile.Core.ViewModels
{
    /// <summary>
    /// ViewModel for login page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class SimpleLoginPageModel : LoginViewModel
    {
        #region Fields

        private ValidatableObject<string> password;
        private readonly IAuthenticationService _authenticationService;
        private readonly ISettingsService _settingsService;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="SimpleLoginPageModel" /> class.
        /// </summary>
        // public SimpleLoginPageViewModel()
        // {
        //     this.InitializeProperties();
        //     this.AddValidationRules();
        //     this.LoginCommand = new Command(this.LoginClicked);
        //     this.SignUpCommand = new Command(this.SignUpClicked);
        //     this.ForgotPasswordCommand = new Command(this.ForgotPasswordClicked);
        //     this.SocialMediaLoginCommand = new Command(this.SocialLoggedIn);
        // }

        public SimpleLoginPageModel(IAuthenticationService authenticationService, ISettingsService settingsService)
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
                // Call Api for authentication
                var authenticationResponse = await _authenticationService.Authenticate(Email.Value, Password.Value);

                if (authenticationResponse.IsAuthenticated)
                {
                    // we store the Id to know if the user is already logged in to the application
                    _settingsService.UserIdSetting = authenticationResponse.User.Id;
                    _settingsService.UserNameSetting = authenticationResponse.User.FirstName;
                    await Shell.Current.DisplayAlert("Welcome back!", "", "Ok");
                    // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
                    await Shell.Current.GoToAsync("//About");
                }

            }
        }

        /// <summary>
        /// Invoked when the Sign Up button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void SignUpClicked(object obj)
        {
            // Do Something
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
        private void SocialLoggedIn(object obj)
        {
            // Do something

        }

        #endregion
    }
}