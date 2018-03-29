using System;
using Xamarin.Forms;
using System.Linq;

namespace DOH2015.StaticResources
{
	public class TimeWithSecondToHourMinuteConverter : IValueConverter
	{
		public object Convert (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			var val = value as string;
			if (val != null) {
				return val.Substring(0, val.Length - 3);		
			}
			throw new InvalidCastException ();
		}

		public object ConvertBack (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException ();
		}
	}
}

