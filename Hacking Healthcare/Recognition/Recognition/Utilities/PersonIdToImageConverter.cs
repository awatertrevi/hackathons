using System;
using System.Globalization;
using Xamarin.Forms;
using Realms;
using Recognition.Models;
using System.Linq;
using System.IO;

namespace Recognition.Utilities
{
	public class PersonIdToImageConverter : IValueConverter
	{
		private Realm realmInstance = Realm.GetInstance();

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var personId = value as string;

			var savedPersonImage = realmInstance.All<SavedPersonImage>().SingleOrDefault(spi => spi.personId == personId);

			if (savedPersonImage == null)
				return ImageSource.FromFile("no_user.png");

			var array = System.Convert.FromBase64String(savedPersonImage.image);

			return ImageSource.FromStream(() => new MemoryStream(array));
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}
