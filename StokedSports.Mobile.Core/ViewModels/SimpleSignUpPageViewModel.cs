using System;
using System.Diagnostics;
using System.Threading.Tasks;
using StokedSports.Mobile.Core.Services.General;
using StokedSports.Mobile.Core.Validators;
using StokedSports.Mobile.Core.Validators.Rules;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace StokedSports.Mobile.Core.ViewModels
{
    /// <summary>
    /// ViewModel for sign-up page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class SimpleSignUpPageViewModel : LoginViewModel
    {
        #region Fields

        private ValidatableObject<string> userName;

        private ValidatablePair<string> password;

        private readonly IAuthenticationService _authenticationService;
        private readonly ISettingsService _settingsService;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="SimpleSignUpPageViewModel" /> class.
        /// </summary>
        public SimpleSignUpPageViewModel(IAuthenticationService authenticationService, ISettingsService settingsService)
        {
            _authenticationService = authenticationService;
            _settingsService = settingsService;
            this.InitializeProperties();
            this.AddValidationRules();
            this.LoginCommand = new Command(this.LoginClicked);
            this.SignUpCommand = new Command(this.SignUpClicked);
        }

        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the userName from user in the Sign Up page.
        /// </summary>
        public ValidatableObject<string> UserName
        {
            get
            {
                return this.userName;
            }

            set
            {
                if (this.userName == value)
                {
                    return;
                }

                this.SetProperty(ref this.userName, value);
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the password from users in the Sign Up page.
        /// </summary>
        public ValidatablePair<string> Password
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
        #endregion

        #region Methods

        /// <summary>
        /// Initialize whether fieldsvalue are true or false.
        /// </summary>
        /// <returns>true or false </returns>
        public bool AreFieldsValid()
        {
            bool isEmail = this.Email.Validate();
            bool isNameValid = this.UserName.Validate();
            bool isPasswordValid = this.Password.Validate();

            return isPasswordValid && isNameValid && isEmail;
        }

        /// <summary>
        /// Initializing the properties.
        /// </summary>
        private void InitializeProperties()
        {
            this.UserName = new ValidatableObject<string>();
            this.Password = new ValidatablePair<string>();
        }

        /// <summary>
        /// this method contains the validation rules
        /// </summary>
        private void AddValidationRules()
        {
            this.UserName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "UserName Required" });
            this.Password.Item1.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Password Required" });
            this.Password.Item2.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Re-enter Password" });
        }

        /// <summary>
        /// Invoked when the Log in button is clicked.
        /// </summary>
        /// <param userName="obj">The Object</param>
        private async void LoginClicked(object obj)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }

        /// <summary>
        /// Invoked when the Sign Up button is clicked.
        /// </summary>
        /// <param userName="obj">The Object</param>
        private async void SignUpClicked(object obj)
        {
            try
            {
                if (this.AreFieldsValid())
                {
                    //Display ActivityIndicator while processing
                    IsBusy = true;

                    var userRegistered =
                        await _authenticationService.Register(UserName.Value, Email.Value, Password.Item1.Value);
                    if (userRegistered.IsAuthenticated)
                    {
                        await Shell.Current.DisplayAlert("Registration successful", "Message", "OK");
                        _settingsService.UserIdSetting = userRegistered.User.Id;
                        // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
                        await Shell.Current.GoToAsync("//LoginPage");
                    }
                }
            }
            catch (Exception e) //TODO: Move this to global exception handling
            {
                var message = $"Error occurred during Signup: {e.Message} :: {e}";
                Debug.Write(message);
                await Shell.Current.DisplayAlert("Oops an error occurred during Signup, please try again!", "Message", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        #endregion
    }
}