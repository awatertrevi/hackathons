using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pakkie.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class InfoLabel : ContentView
	{
		public InfoLabel ()
		{
		    BindingContext = this;
			InitializeComponent ();
		}

	    public static readonly BindableProperty TitleProperty =
	        BindableProperty.Create("Title", typeof(string), typeof(InfoLabel), null);

	    public string Title
	    {
            get => (string) GetValue(TitleProperty);
	        set => SetValue(TitleProperty, value);
	    }

	    public static readonly BindableProperty MessageProperty =
	        BindableProperty.Create("Message", typeof(string), typeof(InfoLabel), null);

	    public string Message
        {
	        get => (string)GetValue(MessageProperty);
	        set => SetValue(MessageProperty, value);
	    }
    }
}