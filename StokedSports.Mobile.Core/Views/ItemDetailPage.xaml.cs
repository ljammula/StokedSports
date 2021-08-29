using StokedSports.Mobile.Core.ViewModels;
using Xamarin.Forms;

namespace StokedSports.Mobile.Core.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}