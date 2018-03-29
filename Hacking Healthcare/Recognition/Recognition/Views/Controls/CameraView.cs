using System;
using Xamarin.Forms;
using System.ComponentModel;
namespace Recognition.Views.Controls
{
	public class CameraView : ContentView, INotifyPropertyChanged
	{
		public bool TimerRunning { get; set; }

		public delegate void PhotoResultEventHandler(PhotoResultEventArgs result);

		public event PhotoResultEventHandler OnPhotoResult;

		public void SetPhotoResult(byte[] image, int width = -1, int height = -1)
		{
			OnPhotoResult?.Invoke(new PhotoResultEventArgs(image, width, height));
		}
	}
}
