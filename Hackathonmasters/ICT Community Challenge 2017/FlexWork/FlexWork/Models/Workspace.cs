using System;
using Xamarin.Forms.Maps;

namespace FlexWork.Models
{
	public partial class Workspace
	{
		public Position Position => new Position(Latitude, Longitude);
	}
}
