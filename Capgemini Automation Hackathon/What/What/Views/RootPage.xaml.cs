using System;
using System.Collections.Generic;
using FoundIt.ViewModels;
using Xamarin.Forms;
using What.Cells;
using ZXing.Net.Mobile.Forms;
using ZXing.Mobile;
using System.Linq;
using Speech;
using AVFoundation;
using Foundation;
using What.Models;
using MapKit;
using UIKit;
using LocalAuthentication;

namespace What
{
    public partial class RootPage : ContentPage
    {
        private string[] Names = { "Bartel", "Bob", "Martin", "Trevi", "Josh" };
        private Random rand = new Random();
        void Handle_Clicked(object sender, EventArgs e)
        {
            ViewModel.AddMessage(new Message()
            {
                Text = $"I called {Names[rand.Next(Names.Length)]} for you.\n\nHe will be with you in a couple of minutes.",
                Incomming = true,
                MessageDateTime = DateTime.Now
            });
        }

        private RootPageViewModel ViewModel => (RootPageViewModel)BindingContext;
        private bool dirtyFlag = true;

        public RootPage()
        {
            InitializeComponent();

            BindingContext = new RootPageViewModel();

            MessagingCenter.Subscribe<RootPageViewModel>(this, "ScrollDown", (obj) =>
            {
                MessagesListView.ScrollTo(ViewModel.Messages.Last(), ScrollToPosition.End, true);
            });

            MessagingCenter.Subscribe<IncommingCell>(this, "SendNavigationMessage", (obj) =>
            {
                ViewModel.AddMessage(

            new Message()
            {
                Text = "It’s a 13 min walk to HEMA, should I start navigation?",
                YesNoQuestion = true,
                Incomming = true,
                Id = 1
            });
            });

            MessagingCenter.Subscribe<IncommingCell, int>(this, "SendYes", (obj, param) =>
            {
                if (param == 1)
                {
                    ViewModel.Messages.Add(new Message()
                    {
                        Text = "Yes",
                        MessageDateTime = DateTime.Now
                    });

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        var coordinate = new CoreLocation.CLLocationCoordinate2D(52.092698, 5.012018);
                        var address = new MKPlacemarkAddress();
                        var mapPlace = new MKPlacemark(coordinate, address);
                        var mapItem = new MKMapItem(mapPlace);
                        mapItem.Name = "HEMA Utrecht";
                        mapItem.OpenInMaps();

                        ViewModel.Messages.Add(new Message()
                        {
                            Text = "HEMA Floorplan\n\nFollow the blue arrow.",
                            Incomming = true,
                            FloorPlan = true,
                            MessageDateTime = DateTime.Now
                        });

                        ViewModel.AddMessage(new Message()
                        {
                            Text = "Do you want to purchase the product or do you have another question?",
                            Incomming = true,
                            MessageDateTime = DateTime.Now
                        });
                    });

                    MessagesListView.ScrollTo(ViewModel.Messages.Last(), ScrollToPosition.End, true);

                    var notification = new UILocalNotification();
                    notification.FireDate = NSDate.FromTimeIntervalSinceNow(TimeSpan.FromSeconds(7).TotalSeconds);
                    notification.AlertBody = "Welcome at HEMA, starting indoor navigation.";
                    notification.SoundName = UILocalNotification.DefaultSoundName;

                    UIApplication.SharedApplication.ScheduleLocalNotification(notification);
                }

                else if (param == 2)
                {
                    ViewModel.AddMessage(new Message()
                    {
                        Text = $"I called {Names[rand.Next(Names.Length)]} for you.\n\nHe will be with you in a couple of minutes.",
                        Incomming = true,
                        MessageDateTime = DateTime.Now
                    });
                }
            });

            MessagingCenter.Subscribe<IncommingCell>(this, "SendNo", (obj) =>
            {
                ViewModel.Messages.Add(new Message()
                {
                    Text = "No",
                    MessageDateTime = DateTime.Now
                });

                MessagesListView.ScrollTo(ViewModel.Messages.Last(), ScrollToPosition.End, true);
            });

            MessagingCenter.Subscribe<IncommingCell>(this, "OpenScanner", async (obj) =>
             {
                 var options = new MobileBarcodeScanningOptions
                 {
                     TryHarder = true,
                     AutoRotate = true,
                     PossibleFormats = new List<ZXing.BarcodeFormat>
                     {
                        ZXing.BarcodeFormat.EAN_8, ZXing.BarcodeFormat.EAN_13
                    }
                 };

                 var scanPage = new ZXingScannerPage(options)
                 {
                     Title = "Scanner"
                 };

                 scanPage.OnScanResult += (result) =>
                 {
                     if (dirtyFlag == false)
                         return;

                     dirtyFlag = false;

                     // Stop scanning
                     scanPage.IsScanning = false;

                     // Pop the page and show the result
                     Device.BeginInvokeOnMainThread(async () =>
                      {
                          await Navigation.PopAsync();

                          ViewModel.AddMessage(new Message()
                          {
                              Incomming = true,
                              Text = "That would be €4,-\n\nPlease confirm your payment with your Touch ID."
                          });

                          var context = new LAContext();
                          NSError AuthError;
                          var myReason = new NSString("Confirm your payment.");

                          if (context.CanEvaluatePolicy(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, out AuthError))
                          {
                              var replyHandler = new LAContextReplyHandler((success, error) =>
                              {
                                  if (success)
                                  {
                                      ViewModel.AddMessage(new Message()
                                      {
                                          Text = "Payment confirmed. Put your phone next to the shopping tag in order to remove the security seal.",
                                          Incomming = true,
                                          MessageDateTime = DateTime.Now
                                      });
                                  }
                              });

                              context.EvaluatePolicy(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, myReason, replyHandler);

                          }
                      });
                 };

                 dirtyFlag = true;

                 await Navigation.PushAsync(scanPage);
             });
        }
        bool firstLaunch = true;
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (firstLaunch == false)
                return;

            firstLaunch = false;
            mic.IsEnabled = false;

            SFSpeechRecognizer.RequestAuthorization((SFSpeechRecognizerAuthorizationStatus auth) =>
            {
                bool buttonIsEnabled = false;
                switch (auth)
                {
                    case SFSpeechRecognizerAuthorizationStatus.Authorized:
                        buttonIsEnabled = true;
                        var node = audioEngine.InputNode;
                        var recordingFormat = node.GetBusOutputFormat(0);
                        node.InstallTapOnBus(0, 1024, recordingFormat, (AVAudioPcmBuffer buffer, AVAudioTime when) =>
                        {
                            recognitionRequest.Append(buffer);
                        });
                        break;
                    case SFSpeechRecognizerAuthorizationStatus.Denied:
                        buttonIsEnabled = false;
                        break;
                    case SFSpeechRecognizerAuthorizationStatus.Restricted:
                        buttonIsEnabled = false;
                        break;
                    case SFSpeechRecognizerAuthorizationStatus.NotDetermined:
                        buttonIsEnabled = false;
                        break;
                }

                Device.BeginInvokeOnMainThread(() => { mic.IsEnabled = buttonIsEnabled; });
            });
        }

        private SFSpeechAudioBufferRecognitionRequest recognitionRequest;
        private SFSpeechRecognitionTask recognitionTask;
        private AVAudioEngine audioEngine = new AVAudioEngine();
        private SFSpeechRecognizer speechRecognizer = new SFSpeechRecognizer(new NSLocale("en_US"));

        bool recording = false;

        async void Handle_Tapped(object sender, System.EventArgs e)
        {
            if (recording == false)
            {
                recording = true;
                label.IsVisible = true;
                StartSpeechRecognition();

                while (recording)
                {
                    try
                    {
                        await mic.ScaleTo(1.25, 500);
                        await mic.ScaleTo(1.0, 250);
                    }

                    catch
                    {

                    }
                }

                label.IsVisible = false;
            }

            else
            {
                recording = false;
                StopSpeechRecognition();
            }
        }

        public void StopSpeechRecognition()
        {
            audioEngine.Stop();
            recognitionRequest.EndAudio();
        }

        public void StartSpeechRecognition()
        {
            recognitionRequest = new SFSpeechAudioBufferRecognitionRequest();

            audioEngine.Prepare();
            NSError error;
            audioEngine.StartAndReturnError(out error);
            if (error != null)
            {
                DisplayAlert("Oops!", "Something went wrong while starting the microphone. Please try again!", "OK");
                return;
            }
            recognitionTask = speechRecognizer.GetRecognitionTask(recognitionRequest, (SFSpeechRecognitionResult result, NSError err) =>
            {
                if (err != null)
                {
                    ViewModel.AddMessage(new Message()
                    {
                        Incomming = true,
                        MessageDateTime = DateTime.Now,
                        Text = "Oops, I didn't get that. Could you repeat that?"
                    });
                }
                else
                {
                    if (result.Final == true)
                    {
                        ViewModel.SendCommand.Execute(result.BestTranscription.FormattedString);
                    }
                }
            });
        }
    }
}
