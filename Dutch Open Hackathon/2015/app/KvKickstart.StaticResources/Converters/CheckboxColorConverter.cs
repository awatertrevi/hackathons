using System;
using Xamarin.Forms;

namespace DOH2015.StaticResources
{
	public class CheckboxColorConverter : IValueConverter
	{
		public object Convert (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value is bool) {
				return (bool)value ? Color.Gray : Color.Black;
			}
			throw new InvalidCastException ();
		}

		public object ConvertBack (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException ();
		}
	}
}

