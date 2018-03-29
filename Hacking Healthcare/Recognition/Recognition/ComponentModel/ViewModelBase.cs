//
// ViewModelBase.cs
//
// Trevi Awater (t.awater@resoftware.nl)
// 11/18/2016
//

using Xamarin.Forms;

namespace Recognition.ComponentModel
{
	public abstract class ViewModelBase : ObservableBase
	{
		protected INavigation Navigation { get; private set; }

		public ViewModelBase(INavigation navigation)
		{
			Navigation = navigation;
		}

		public bool IsSaving
		{
			get { return get(() => this.IsSaving); }

			set
			{
				set(() => this.IsSaving, value);

				onPropertyChanged("IsBusy");
			}
		}

		public bool IsLoading
		{
			get { return get(() => this.IsLoading); }

			set
			{
				set(() => this.IsLoading, value);

				onPropertyChanged("IsBusy");
			}
		}

		public bool IsBusy => IsSaving || IsLoading;
	}
}