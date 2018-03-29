//
// LoginPage.xaml.cs
//
// Trevi Awater (t.awater@resoftware.nl)
// 11/18/2016
//

using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FindItApp.Utilities;
using FlexWork.Utilities.Helpers;
using FlexWork.ViewModels;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace FlexWork.Views.Pages
{
	public partial class LoginPage : ContentPage
	{
		private LoginPageViewModel ViewModel => (LoginPageViewModel)BindingContext;

		public LoginPage()
		{
			InitializeComponent();

			BindingContext = new LoginPageViewModel(Navigation);

			App.PostSuccessFacebookAction = async (token) =>
			{
				if (!await ViewModel.DispatchFacebookToken(token))
				{
					App.SetTabTo(TabPages.Map);

					await DisplayAlert("Oeps!", "Er ging iets mis tijdens het inloggen, probeer het later opnieuw.", "OK");
				}

				await NavigationHandler.PopModalAsync(Navigation, Color.White);
			};

			MessagingCenter.Subscribe<LoginPageViewModel, string>(this, "Error", async (sender, message) =>
			{
				await DisplayAlert("Oeps!", message, "OK");
			});
		}

		protected override void OnAppearing()
		{
			ApplicationContext.Current.SignoutFacebook();

			base.OnAppearing();
		}

		protected override bool OnBackButtonPressed()
		{
			App.SetTabTo(TabPages.Map);

			return base.OnBackButtonPressed();
		}
	}
}