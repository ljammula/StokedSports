using System;
using StokedSports.Mobile.Core.Views;
using Xamarin.Forms;

namespace StokedSports.Mobile.Core
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            FlyoutIsPresented = false;
            await DisplayAlert("Logout", "Successfully Logout!", "Ok");
            await Shell.Current.GoToAsync("//LoginPage");
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            FlyoutIsPresented = false;
            await DisplayAlert("Login", "Welcome back!", "Ok");
            await Shell.Current.GoToAsync("//About");
        }
    }
}
