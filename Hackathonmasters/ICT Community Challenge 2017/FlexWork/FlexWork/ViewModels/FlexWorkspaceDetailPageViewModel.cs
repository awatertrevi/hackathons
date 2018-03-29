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
	public partial class FlexWorkspaceDetailPageViewModel : ViewModelBase
	{
		public Workspace SelectedWorkspace { get { return get(() => this.SelectedWorkspace); } set { set(() => this.SelectedWorkspace, value); } }
	}
}