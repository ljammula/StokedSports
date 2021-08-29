using StokedSports.Mobile.Core.Models;
using StokedSports.Mobile.Core.ViewModels;
using Xamarin.Forms;

namespace StokedSports.Mobile.Core.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}