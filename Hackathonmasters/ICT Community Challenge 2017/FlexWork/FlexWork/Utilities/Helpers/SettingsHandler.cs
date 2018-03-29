//
// SettingsHandler.cs
//
// Trevi Awater (t.awater@resoftware.nl)
// 11/18/2016
//

using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;

namespace FlexWork.Utilities.Helpers
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

		private const string CheckinLocationKey = "checkin_location";
		private static readonly string CheckinLocationDefault = null;

		private const string CheckinIdKey = "checkin_id";
		private static readonly Guid CheckinIdDefault = Guid.Empty;

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

		public static string CheckinLocation
		{
			get
			{
				return AppSettings.GetValueOrDefault(CheckinLocationKey, CheckinLocationDefault);
			}

			set
			{
				AppSettings.AddOrUpdateValue(CheckinLocationKey, value);
			}
		}

		public static Guid CheckinId
		{
			get
			{
				return AppSettings.GetValueOrDefault(CheckinIdKey, CheckinIdDefault);
			}

			set
			{
				AppSettings.AddOrUpdateValue(CheckinIdKey, value);
			}
		}
	}
}