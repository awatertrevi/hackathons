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
using Plugin.Geolocator;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace FlexWork.ViewModels
{
	public partial class WorkspacesPageViewModel
	{
		public WorkspacesPageViewModel(INavigation navigation) : base(navigation)
		{
			DirtySelection = "Dichtbij";

			LoadWorkspaces = new Command(async () =>
			{
				IsRefreshing = true;

				if (DirtySelection == "Dichtbij")
				{
					var geolocator = CrossGeolocator.Current;
					var position = await geolocator.GetPositionAsync(10000);

					var workspaceResults = await ApplicationContext.Current.GetNearbyWorkspaces(position.Latitude, position.Longitude, 10);

					if (workspaceResults == null || workspaceResults.Success == false)
						return;

					Workspaces = workspaceResults.Result;
				}

				else if (DirtySelection == "Recent")
				{
					var workspaceResults = await ApplicationContext.Current.GetRecentWorkspaces();

					if (workspaceResults == null || workspaceResults.Success == false)
						return;

					Workspaces = workspaceResults.Result;
				}

				IsRefreshing = false;
			});

			if (LoadWorkspaces.CanExecute(null))
				LoadWorkspaces.Execute(null);
		}
	}
}
