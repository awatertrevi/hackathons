//
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
using FlexWork.Models;

namespace FlexWork.ViewModels
{
	public partial class FlexWorkspaceDetailPageViewModel
	{
		public FlexWorkspaceDetailPageViewModel(INavigation navigation, Workspace workspace) : base(navigation)
		{
			SelectedWorkspace = workspace;
		}
	}
}
