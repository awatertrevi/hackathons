//
// SettingsHandler.cs
//
// Trevi Awater (t.awater@resoftware.nl)
// 11/18/2016
//

using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace FoundIt.Utilities.Helpers
{
    public class SettingsHandler
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string AccessTokenKey = "access_token";
        private static readonly string AccessTokenDefault = string.Empty;

        #endregion

        public static string AccessToken
        {
            get
            {
                return AppSettings.GetValueOrDefault(AccessTokenKey, AccessTokenDefault);
            }

            set
            {
                AppSettings.AddOrUpdateValue(AccessTokenKey, value);
            }
        }
    }
}