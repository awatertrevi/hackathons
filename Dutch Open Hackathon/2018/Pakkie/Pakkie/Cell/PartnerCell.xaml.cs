using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pakkie.Cell
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PartnerCell : ViewCell
	{
		public PartnerCell ()
		{
		    BindingContext = this;
			InitializeComponent ();
		}
	}
}