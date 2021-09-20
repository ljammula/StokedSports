using Plugin.Settings;
using Plugin.Settings.Abstractions;
using StokedSports.Mobile.Core.Models;
using StokedSports.Mobile.Core.Extensions;

namespace StokedSports.Mobile.Core.Utility
{
    public static class AppSettings
    {
        private static ISettings Settings => CrossSettings.Current;

        public static User User
        {
            get => Settings.GetValueOrDefault(nameof(User), default(User));

            set => Settings.AddOrUpdateValue(nameof(User), value);
        }
    }
}
