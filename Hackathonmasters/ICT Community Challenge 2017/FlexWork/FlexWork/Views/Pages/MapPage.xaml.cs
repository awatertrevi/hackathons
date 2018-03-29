using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using FlexWork.ViewModels;
using FlexWork.Views.Controls;
using FlexWork.Models;
using FlexWork.Utilities.Helpers;
using FlexWork.Views.Pages;
using Plugin.Geolocator;
using ImageCircle.Forms.Plugin.Abstractions;
using System.Windows.Input;

namespace FlexWork
{
	public partial class MapPage : ContentPage
	{
		private MapPageViewModel ViewModel => (MapPageViewModel)BindingContext;

		public MapPage()
		{
			InitializeComponent();

			BindingContext = new MapPageViewModel(Navigation);

			map.PropertyChanged += async (sender, e) =>
			{
				if (e.PropertyName == nameof(map.VisibleRegion))
				{
					var workspaceResults = await ApplicationContext.Current.GetNearbyWorkspaces(map.VisibleRegion.Center.Latitude, map.VisibleRegion.Center.Longitude, map.VisibleRegion.Radius.Kilometers * 1.35);

					if (workspaceResults == null || workspaceResults.Success == false)
						return;

					ViewModel.Workspaces = workspaceResults.Result;

					DrawMapPins();
				}
			};
		}

		bool firstLaunch = true, tutorialShown = false;

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			if (firstLaunch)
			{
				await FocusOnUser();
				firstLaunch = false;
			}

			if (map.VisibleRegion.Radius.Kilometers > 20)
				await FocusOnUser();

			if (tutorialShown == false)
			{
				var demoPage = new DemoPage(new List<DemoItem>()
				{
					new DemoItem()
					{
						Title = "OVERZICHT",
						Image = "map_preview.png",
						Text = "Vind de ideale flex werkplek bij jou in de buurt!"
					},

					new DemoItem()
					{
						Title = "INFORMATIE",
						Image = "workspace_preview.png",
						Text = "Bekijk welke faciliteiten beschikbaar zijn gesteld in deze werkplek! "
					},

					new DemoItem()
					{
						Title = "10 OUT OF 10 WOULD FLEX AGAIN!",
						Image = "rate_preview.png",
						Text = "Beoordeel jouw meest recente flex werkplek!"
					},

					new DemoItem()
					{
						Title = "IN DE BUURT",
						Image = "start_preview.png",
						Text = "Zie in waar de dichtsbijzijnde flex werkplekken zich bevinden en waar jij voor het laatst bent ingecheckt!",
						HasButton = true,
						ButtonClickCommand = new Command(async () =>
						{
							await NavigationHandler.PopModalAsync(Navigation, Color.White);
						}),
						ButtonText = "GA AAN DE SLAG!",
						ButtonTextColor = Color.White,
						ButtonBackgroundColor = Color.FromHex("#00c1d5")
					},
				});

				Device.BeginInvokeOnMainThread(async () =>
				{
					await NavigationHandler.PushModalAsync(Navigation, demoPage, Color.White);
				});

				tutorialShown = true;
			}

			if (!string.IsNullOrEmpty(SettingsHandler.CheckinLocation))
			{
				checkinText.Text = "Momenteel ingecheckt bij: " + SettingsHandler.CheckinLocation;
				checkinActive.IsVisible = true;
			}

			else
			{
				checkinActive.IsVisible = false;
			}
		}

		private async Task FocusOnUser()
		{
			var geolocator = CrossGeolocator.Current;

			var position = await geolocator.GetPositionAsync(10000);

			map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude), Distance.FromKilometers(2)));
		}

		async void Handle_Tapped(object sender, System.EventArgs e)
		{
			var workspace = await ApplicationContext.Current.GetWorkspace(SettingsHandler.CheckinId);

			await NavigationHandler.PushAsync(Navigation, new CheckoutPage(workspace.Result));
		}

		private void DrawMapPins()
		{
			var workoutPins = map.Pins.Where(p => p.GetId() is Workspace).ToArray();

			if (ViewModel.Workspaces != null)
			{
				foreach (var workspace in ViewModel.Workspaces.Where(i => !workoutPins.Any(p => i.Id == p.GetId<Workspace>().Id)))
				{
					var pin = new Pin()
					{
						Label = workspace.Name ?? string.Empty,
						Address = workspace.Address ?? string.Empty,
						Position = workspace.Position
					};

					pin.SetId(workspace);
					pin.SetImage(new FileImageSource()
					{
						File = workspace.CompanyType == CompanyType.Corporate ? "organisation.png" : "small-business.png"
					});

					var facilitiesStack = new StackLayout() { Orientation = StackOrientation.Horizontal, HorizontalOptions = LayoutOptions.Center };

					foreach (var facility in workspace.WorkspaceFacilities)
					{
						facilitiesStack.Children.Add(new Button()
						{
							Image = facility.Facility.Icon,
							HeightRequest = 32,
							WidthRequest = 32,
							BorderRadius = 16,
							BackgroundColor = Color.FromHex("#AC145A"),
							IsEnabled = false
						});
					}

					pin.SetCalloutView(new StackLayout()
					{
						WidthRequest = 200,
						Children =
						{
							new Image() { Source = workspace.Image, Aspect = Aspect.AspectFit },
							facilitiesStack,
							new BoxView()
							{
								BackgroundColor = Color.Gray,
								HeightRequest = 1,
								Margin = new Thickness(15, 0)
							},
							new StackLayout()
							{
								Padding = 10,
								Children =
								{
									new Label()
									{
										FontAttributes = FontAttributes.Bold,
										FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
										TextColor = Color.Black,
										Text = workspace.Name
									},
									new Label()
									{
										FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
										TextColor = Color.Gray,
										Text = workspace.FullAddress
									},
									new Label()
									{
										FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
										TextColor = Color.Gray,
										Text = workspace.CutOfDescription
									},

									new StackLayout()
									{
										Orientation = StackOrientation.Horizontal,
										Children =
										{
											new StackLayout()
											{
												HorizontalOptions = LayoutOptions.Start,
												Orientation = StackOrientation.Horizontal,
												Children =
												{
													new Image()
													{
														Source = "amel.png",
														HeightRequest = 32,
													},

													new Label()
													{
														Text = "+5",
														FontSize = 10,
														VerticalOptions = LayoutOptions.Center
													}
												}
											},

											new StackLayout()
											{
												HorizontalOptions = LayoutOptions.EndAndExpand,
												VerticalOptions = LayoutOptions.Center,
												Orientation = StackOrientation.Horizontal,
												Spacing = 3,
												Children =
												{
													new Image()
													{
														Source = "star_filled.png",
														WidthRequest = 15,
														HeightRequest = 15
													},

													new Image()
													{
														Source = "star_filled.png",
														WidthRequest = 15,
														HeightRequest = 15
													},

													new Image()
													{
														Source = "star_filled.png",
														WidthRequest = 15,
														HeightRequest = 15
													},

													new Image()
													{
														Source = "star_half.png",
														WidthRequest = 15,
														HeightRequest = 15
													},

													new Image()
													{
														Source = "star_empty.png",
														WidthRequest = 15,
														HeightRequest = 15
													}
												}
											}
										}
									}
								}
							}
						},

						GestureRecognizers =
						{
							new TapGestureRecognizer()
							{
								Command = new Command(async () =>
								{
									await NavigationHandler.PushAsync(Navigation, new FlexWorkspaceDetailPage(workspace));
								})
							}
						}
					});

					map.Pins.Add(pin);
				}

				foreach (var pin in workoutPins.Where(p => !ViewModel.Workspaces.Any(i => i.Id == p.GetId<Workspace>().Id)))
				{
					pin.SetId(null);
					map.Pins.Remove(pin);
				}
			}
		}

		private async void StartSearch(object sender, EventArgs e)
		{
			var searchBar = (SearchBar)sender;

			await MoveToAddress(searchBar.Text);
		}

		private async Task MoveToAddress(string location)
		{
			var geocoder = new Geocoder();
			var positions = await geocoder.GetPositionsForAddressAsync(location);
			var position = positions.FirstOrDefault();

			map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude), Distance.FromKilometers(2)));
		}
	}
}
