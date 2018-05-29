using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pakkie.Framework;
using Pakkie.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pakkie.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OnboardingPage : PageBase<OnboardingViewModel>
	{
		public OnboardingPage ()
		{
		    ViewModel = GetViewModel();
		    BindingContext = ViewModel;
			InitializeComponent ();
		}

	    private OnboardingViewModel GetViewModel()
	    {
	        return new OnboardingViewModel
	        {
	            SwipeLeftCommand = new Command(MoveOnboardingRight),
	            SwipeRightCommand = new Command(MoveOnboardingLeft)
	        };
	    }

	    private void MoveOnboardingLeft()
	    {
	        throw new NotImplementedException();
	    }

	    private void MoveOnboardingRight()
	    {
	        throw new NotImplementedException();
	    }
	}
}