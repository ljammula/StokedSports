using StokedSports.Mobile.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StokedSports.Mobile.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DefaultLoginPage : ContentPage
    {
        public DefaultLoginPage()
        {
            InitializeComponent();
            this.BindingContext = new DefaultLoginViewModel();
        }
    }
}