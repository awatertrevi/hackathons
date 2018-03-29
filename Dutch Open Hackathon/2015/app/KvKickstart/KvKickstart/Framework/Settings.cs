using System;
using Refractored.Xam.Settings.Abstractions;
using Refractored.Xam.Settings;
using DOH2015.Models;

namespace DOH2015
{
	public static class Settings
	{
		private static ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
			}
		}

		private const string RegisteredForEventKey = "registeredforevent_key";
		private const bool RegisteredForEventDefault = false;

		public static bool RegisteredForEvent{
			get { return AppSettings.GetValueOrDefault<bool> (RegisteredForEventKey, RegisteredForEventDefault); }
			set { AppSettings.AddOrUpdateValue<bool> (RegisteredForEventKey, value); }
		}


		private const string SelectedCityKey ="selectedCity_key";
		private const string SelectedCityDefault = null;

		public static string SelectedCity{
			get { return AppSettings.GetValueOrDefault<string> (SelectedCityKey, SelectedCityDefault); }
			set { AppSettings.AddOrUpdateValue<string> (SelectedCityKey, value); }
		}

		private const string SelectedPresentationsKey ="selectedPresentations_key";
		private const string SelectedPresentationsDefault = "";

		public static string SelectedPresentations{
			get { return AppSettings.GetValueOrDefault<string> (SelectedPresentationsKey, SelectedPresentationsDefault); }
			set { AppSettings.AddOrUpdateValue<string> (SelectedPresentationsKey, value); }
		}
	}
}

