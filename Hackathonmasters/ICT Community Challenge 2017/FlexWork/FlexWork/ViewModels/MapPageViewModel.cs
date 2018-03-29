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
	public partial class MapPageViewModel : ViewModelBase
	{
		public ObservableCollection<Workspace> Workspaces { get { return get(() => this.Workspaces); } set { set(() => this.Workspaces, value); } }
	}
}