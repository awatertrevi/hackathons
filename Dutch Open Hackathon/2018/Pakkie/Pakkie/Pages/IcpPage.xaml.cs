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
	public partial class IcpPage : PageBase<IcpViewModel>
	{
		public IcpPage ()
		{
		    ViewModel = new IcpViewModel
		    {
		        NextCommand = new Command(async () =>
		        {
		            var page = new DeliveryPage();
		            await page.Init();
		            await Navigation.PushAsync(page);
		        })
		    };
		    BindingContext = ViewModel;
			InitializeComponent ();
		}

	    private void ListView_NoItemSelection(object sender, SelectedItemChangedEventArgs e)
	    {
	        ((ListView)sender).SelectedItem = null;
	    }
    }
}