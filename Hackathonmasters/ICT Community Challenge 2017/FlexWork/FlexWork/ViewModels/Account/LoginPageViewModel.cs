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
	public partial class LoginPageViewModel : ViewModelBase
	{
		public ICommand NavigateToPrivacyPolicy { get { return get(() => this.NavigateToPrivacyPolicy); } set { set(() => this.NavigateToPrivacyPolicy, value); } }
		public ICommand NavigateBack { get { return get(() => this.NavigateBack); } set { set(() => this.NavigateBack, value); } }
	}
}