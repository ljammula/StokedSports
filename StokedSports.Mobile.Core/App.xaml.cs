using StokedSports.Mobile.Core.Bootstrap;
using StokedSports.Mobile.Core.Services;
using Xamarin.Forms;

[assembly: ExportFont("Montserrat-Bold.ttf",Alias="Montserrat-Bold")]
     [assembly: ExportFont("Montserrat-Medium.ttf", Alias = "Montserrat-Medium")]
     [assembly: ExportFont("Montserrat-Regular.ttf", Alias = "Montserrat-Regular")]
     [assembly: ExportFont("Montserrat-SemiBold.ttf", Alias = "Montserrat-SemiBold")]
     [assembly: ExportFont("UIFontIcons.ttf", Alias = "FontIcons")]
namespace StokedSports.Mobile.Core
{
    public partial class App : Application
    {

        public App()
        {
            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDg4ODI3QDMxMzkyZTMyMmUzME1aaVFrM2NuQkx5aVJDRWpWcWxKdnlVeDdZeGJhay9IaEhnQVBrd0dQZ3M9");

            InitializeComponent();
            InitializeApp();

            MainPage = new AppShell();
        }

        private void InitializeApp()
        {
            DependencyService.Register<MockDataStore>();
            AppContainer.RegisterDependencies();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
