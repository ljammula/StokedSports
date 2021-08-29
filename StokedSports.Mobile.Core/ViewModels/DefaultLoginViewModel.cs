using StokedSports.Mobile.Core.Views;
using Xamarin.Forms;

namespace StokedSports.Mobile.Core.ViewModels
{
    public class DefaultLoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }

        public DefaultLoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync("//About");
        }
    }
}