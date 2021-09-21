using System.Threading.Tasks;

namespace StokedSports.Mobile.Core.Services.General
{
    public interface IDialogService
    {
        Task ShowDialog(string message, string title, string buttonLabel);

        void ShowToast(string message);
    }
}
