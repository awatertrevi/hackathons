using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pakkie.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pakkie.Components
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OnboardingView : ContentView
	{
		public OnboardingView (OnboardingViewViewModel vm)
		{
		    BindingContext = vm;
			InitializeComponent ();
		}
	}
}