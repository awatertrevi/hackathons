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
using System.Collections.Generic;

namespace FlexWork.ViewModels
{
	public partial class WorkspacesPageViewModel : ViewModelBase
	{
		public bool IsRefreshing { get { return get(() => this.IsRefreshing); } set { set(() => this.IsRefreshing, value); } }

		public string DirtySelection { get { return get(() => this.DirtySelection); } set { set(() => this.DirtySelection, value); } }

		public ICommand LoadWorkspaces { get { return get(() => this.LoadWorkspaces); } set { set(() => this.LoadWorkspaces, value); } }

		public ObservableCollection<Workspace> Workspaces { get { return get(() => this.Workspaces); } set { set(() => this.Workspaces, value); } }
	}
}