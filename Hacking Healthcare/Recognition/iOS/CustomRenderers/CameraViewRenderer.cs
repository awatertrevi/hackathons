using System;
using System.Threading.Tasks;
using AVFoundation;

using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Recognition.Views;
using MyInvisalignSmile.CustomRenderers.iOS;
using System.Timers;
using Recognition.Views.Controls;

[assembly: ExportRenderer(typeof(CameraView), typeof(CameraViewRenderer))]
namespace MyInvisalignSmile.CustomRenderers.iOS
{
	public class CameraViewRenderer : ViewRenderer
	{
		AVCaptureSession captureSession;
		AVCaptureDeviceInput captureDeviceInput;
		AVCaptureStillImageOutput stillImageOutput;
		UIView liveCameraStream;
		public Timer timer = new Timer()
		{
			Interval = 750,
			Enabled = true
		};

		private bool didLoad = false;

		protected override async void OnElementChanged(ElementChangedEventArgs<View> e)
		{
			base.OnElementChanged(e);

			RootPage.CurrentShouldSnapChanged += (shouldSnap) =>
			{
				timer.Enabled = shouldSnap;
			};

			if (didLoad == false)
			{
				Bounds = UIScreen.MainScreen.Bounds;

				StartUserInterface();
				await AuthorizeCameraUse();
				StartLiveCameraStream();

				timer.Elapsed += async (sender, e2) =>
				{
					var data = await CapturePhoto();

					if (data != null)
					{
						UIImage imageInfo = new UIImage(data);

						(Element as CameraView).SetPhotoResult(data.ToArray(),
																	(int)imageInfo.Size.Width,
																	(int)imageInfo.Size.Height);
					}
				};

				didLoad = true;
			}
		}

		private void StartUserInterface()
		{
			liveCameraStream = new UIView()
			{
				Frame = new CGRect(0f, 0f, Bounds.Width, Bounds.Height)
			};

			AddSubview(liveCameraStream);
		}

		public AVCaptureDevice GetCameraForOrientation(AVCaptureDevicePosition orientation)
		{
			var devices = AVCaptureDevice.DevicesWithMediaType(AVMediaType.Video);

			foreach (var device in devices)
			{
				if (device.Position == orientation)
				{
					return device;
				}
			}

			return null;
		}

		public async Task<NSData> CapturePhoto()
		{
			try
			{
				var videoConnection = stillImageOutput.ConnectionFromMediaType(AVMediaType.Video);
				var sampleBuffer = await stillImageOutput.CaptureStillImageTaskAsync(videoConnection);
				var jpegImageAsNsData = AVCaptureStillImageOutput.JpegStillToNSData(sampleBuffer);

				return jpegImageAsNsData;
			}

			catch
			{
				return null;
			}
		}

		public void StartLiveCameraStream()
		{
			captureSession = new AVCaptureSession();

			var videoPreviewLayer = new AVCaptureVideoPreviewLayer(captureSession)
			{
				Frame = liveCameraStream.Bounds
			};
			liveCameraStream.Layer.AddSublayer(videoPreviewLayer);

			var captureDevice = GetCameraForOrientation(AVCaptureDevicePosition.Back);
			ConfigureCameraForDevice(captureDevice);
			captureDeviceInput = AVCaptureDeviceInput.FromDevice(captureDevice);
			var dictionary = new NSMutableDictionary();
			dictionary[AVVideo.CodecKey] = new NSNumber((int)AVVideoCodec.JPEG);

			stillImageOutput = new AVCaptureStillImageOutput()
			{
				OutputSettings = new NSDictionary()
			};

			captureSession.SessionPreset = AVCaptureSession.PresetiFrame1280x720;
			captureSession.AddOutput(stillImageOutput);

			captureSession.AddInput(captureDeviceInput);
			captureSession.StartRunning();
		}

		public void ConfigureCameraForDevice(AVCaptureDevice device)
		{
			var error = new NSError();
			if (device.IsFocusModeSupported(AVCaptureFocusMode.ContinuousAutoFocus))
			{
				device.LockForConfiguration(out error);
				device.FocusMode = AVCaptureFocusMode.ContinuousAutoFocus;
				device.UnlockForConfiguration();
			}
			else if (device.IsExposureModeSupported(AVCaptureExposureMode.ContinuousAutoExposure))
			{
				device.LockForConfiguration(out error);
				device.ExposureMode = AVCaptureExposureMode.ContinuousAutoExposure;
				device.UnlockForConfiguration();
			}
			else if (device.IsWhiteBalanceModeSupported(AVCaptureWhiteBalanceMode.ContinuousAutoWhiteBalance))
			{
				device.LockForConfiguration(out error);
				device.WhiteBalanceMode = AVCaptureWhiteBalanceMode.ContinuousAutoWhiteBalance;
				device.UnlockForConfiguration();
			}
		}

		public async Task AuthorizeCameraUse()
		{
			var authorizationStatus = AVCaptureDevice.GetAuthorizationStatus(AVMediaType.Video);
			if (authorizationStatus != AVAuthorizationStatus.Authorized)
			{
				await AVCaptureDevice.RequestAccessForMediaTypeAsync(AVMediaType.Video);
			}
		}
	}
}