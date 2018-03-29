using System;
using System.Globalization;
using Xamarin.Forms;
using System.Collections.Generic;
using FlexWork.Models;
using System.Linq;
namespace FlexWork.Utilities.Converters
{
	public class OpeningHoursToTodayConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var hours = value as List<OpeningHours>;
			var hour = hours.Single(oh => oh.Day == DateTime.Now.DayOfWeek);

			return hour.IsClosed == true ? "Gesloten" : "Open van " + hour.OpenTime.Value.ToString(@"hh\:mm") + " tot " + hour.CloseTime.Value.ToString(@"hh\:mm");
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
