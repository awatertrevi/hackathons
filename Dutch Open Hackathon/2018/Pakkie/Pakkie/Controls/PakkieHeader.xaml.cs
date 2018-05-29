using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pakkie.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PakkieHeader : ContentView
	{
		public PakkieHeader ()
		{
			InitializeComponent ();
		}
	}
}