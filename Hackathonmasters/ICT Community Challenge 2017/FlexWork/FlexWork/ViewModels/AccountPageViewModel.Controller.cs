﻿//
// LoginPageViewModel.Controller.cs
//
// Trevi Awater (t.awater@resoftware.nl)
// 11/18/2016
//

using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FindItApp.Utilities;
using FlexWork.Utilities.Helpers;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace FlexWork.ViewModels
{
	public partial class AccountPageViewModel
	{
		public AccountPageViewModel(INavigation navigation) : base(navigation)
		{
			CurrentUser = ApplicationContext.Current.CurrentUser;

			ApplicationContext.Current.CurrentUserChanged += (user) =>
			{
				CurrentUser = user;
			};
		}
	}
}
