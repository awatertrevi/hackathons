//
// FacebookLoginButtonRenderer.cs
//
// Trevi Awater (t.awater@resoftware.nl)
// 11/18/2016
//

using System;
using System.Collections.Generic;
using CoreGraphics;
using FlexWork.CustomRenderers.iOS;
using FlexWork.Utilities;
using FlexWork.Utilities.Helpers;
using FlexWork.Views.Controls;
using Facebook.LoginKit;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using FindItApp.Utilities;

[assembly: ExportRenderer(typeof(FacebookLoginButton), typeof(LoginButtonRenderer))]
namespace FlexWork.CustomRenderers.iOS
{
	public class LoginButtonRenderer : ViewRenderer<Button, LoginButton>
	{
		List<string> readPermissions = new List<string> { "email" };
		LoginButton loginView;

		bool hasInitialized;

		protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
		{
			base.OnElementChanged(e);

			if (!hasInitialized)
			{
				loginView = new LoginButton(new CGRect(48, 0, 218, 46))
				{
					LoginBehavior = LoginBehavior.SystemAccount,
					ReadPermissions = readPermissions.ToArray()
				};

				loginView.Completed += async (sender, e2) =>
				{
					if (e2.Error != null)
					{
						var alert = new UIAlertView()
						{
							Title = "Oeps!",
							Message = "Er ging iets mis tijdens het inloggen met Facebook. Probeer het later opnieuw."
						};

						alert.AddButton("OK");
						alert.Show();

						return;
					}

					if (e2.Result.IsCancelled)
					{
						App.SetTabTo(TabPages.Map);

						await NavigationHandler.PopModalAsync(Element.Navigation, Color.White);

						return;
					}

					App.PostSuccessFacebookAction(e2.Result.Token.TokenString);
				};

				loginView.LoggedOut += async (sender, e2) =>
				{
					ApplicationContext.Current.Signout();

					await NavigationHandler.PopModalAsync(Element.Navigation, Color.White);
				};

				if (loginView != null)
					SetNativeControl(loginView);

				hasInitialized = true;
			}
		}
	}
}
