//
// ViewModelBase.cs
//
// Trevi Awater (t.awater@resoftware.nl)
// 11/18/2016
//

using Xamarin.Forms;

namespace FlexWork.ComponentModel
{
	public abstract class ViewModelBase : ObservableBase
	{
		protected INavigation Navigation { get; private set; }

		public ViewModelBase(INavigation navigation)
		{
			Navigation = navigation;
		}
	}
}