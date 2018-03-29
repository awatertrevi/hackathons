using System;
using Xamarin.Forms;
using DOH2015.ComponentModel;
using System.Collections.Generic;
using DOH2015.Models;
using DOH2015.Framework;
using System.Windows.Input;

namespace DOH2015
{
	public class SignUpPageViewModel : ViewModelBase
	{
		INavigation Navigation { get; set; }
		public ICommand RegisterCommand { get { return get (() => this.RegisterCommand); } set { set (() => this.RegisterCommand, value); } }

		public SignUpPageViewModel (INavigation navigation)
		{
			Navigation = navigation;
		}
	}
}



