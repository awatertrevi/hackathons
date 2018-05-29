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
	public partial class ContactCell : ViewCell
	{
		public ContactCell ()
		{
		    BindingContext = this;
			InitializeComponent ();
		}
	}
}