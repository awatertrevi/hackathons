//
// LoginPageViewModel.cs
//
// Trevi Awater (t.awater@resoftware.nl)
// 11/18/2016
//

using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using FlexWork.ComponentModel;
using FlexWork.Models;

namespace FlexWork.ViewModels
{
	public partial class AccountPageViewModel : ViewModelBase
	{
		public User CurrentUser { get { return get(() => this.CurrentUser); } set { set(() => this.CurrentUser, value); } }
	}
}