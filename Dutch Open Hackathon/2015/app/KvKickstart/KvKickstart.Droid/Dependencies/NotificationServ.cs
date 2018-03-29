using System;
using Android.App;
using Android.Content;
using DOH2015;

[assembly: Xamarin.Forms.Dependency (typeof (INotifyServ))]
namespace DOH2015.Droid
{
	public static class NotificationServ
	{
		public static void MyLocalNotification (string title, string text, DateTime time)
		{
			// Instantiate the builder and set notification elements:
			Notification.Builder builder = new Notification.Builder (Application.Context)
					.SetContentTitle (title).SetContentText (text)
				.SetSmallIcon (Resource.Drawable.logo);

			// Build the notification:
			Notification notification = builder.Build ();

			// Get the notification manager:
			NotificationManager notificationManager =
				Application.Context.GetSystemService (Context.NotificationService) as NotificationManager;

			// Publish the notification:
			const int notificationId = 0;
			notificationManager.Notify (notificationId, notification);

			builder.SetWhen (time.Millisecond);
		}
	}
}

