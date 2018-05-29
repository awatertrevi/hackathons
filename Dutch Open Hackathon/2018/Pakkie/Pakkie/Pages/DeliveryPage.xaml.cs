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
	public partial class DeliveryPage : PageBase<DeliveryViewModel>
	{
		public DeliveryPage ()
		{
            ViewModel = new DeliveryViewModel();
		    BindingContext = ViewModel;
			InitializeComponent ();
		}

	    public async Task Init()
	    {
	        await ViewModel.Init();
	    }
	}
}