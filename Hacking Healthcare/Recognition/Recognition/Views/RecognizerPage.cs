using System;
using Xamarin.Forms;
using Recognition.ViewModels;
using System.Net.Http;
using ModernHttpClient;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.IO;
using System.Net.Http.Headers;
using Recognition.Models;
using System.Collections.Generic;
using Recognition.Views.Controls;
using System.Threading;

namespace Recognition.Views
{
	public class RecognizerPage : ContentPage
	{
		public CameraView CameraView = new CameraView();
		private Image previewImage = new Image()
		{
			IsVisible = false
		};

		private Label emoticonLabel = new Label()
		{
			Text = "Unknown",
			TextColor = Color.White,
			HorizontalOptions = LayoutOptions.EndAndExpand
		};

		private Label nameLabel = new Label()
		{
			Text = "Unknown",
			TextColor = Color.White,
			HorizontalOptions = LayoutOptions.EndAndExpand
		};

		private Label relationShipLabel = new Label()
		{
			Text = "Unknown",
			TextColor = Color.White,
			HorizontalOptions = LayoutOptions.EndAndExpand
		};

		public async Task GetEmotions(byte[] array)
		{
			var emotions = new List<KeyValuePair<string, double>>();
			var client = new HttpClient(new NativeMessageHandler());

			client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "70e35f84e60141eebf62f8d0ed78aa1b");

			var content = new ByteArrayContent(array);

			content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
			var requestResult = await client.PostAsync("https://westus.api.cognitive.microsoft.com/emotion/v1.0/recognize", content);

			if (!requestResult.IsSuccessStatusCode)
				return;

			var results = JArray.Parse(await requestResult.Content.ReadAsStringAsync());

			if (results.Count > 0)
			{
				emotions.Add(new KeyValuePair<string, double>("anger", results[0]["scores"].Value<double>("anger")));
				emotions.Add(new KeyValuePair<string, double>("contempt", results[0]["scores"].Value<double>("contempt")));
				emotions.Add(new KeyValuePair<string, double>("disgust", results[0]["scores"].Value<double>("disgust")));
				emotions.Add(new KeyValuePair<string, double>("fear", results[0]["scores"].Value<double>("fear")));
				emotions.Add(new KeyValuePair<string, double>("happiness", results[0]["scores"].Value<double>("happiness")));
				emotions.Add(new KeyValuePair<string, double>("neutral", results[0]["scores"].Value<double>("neutral")));
				emotions.Add(new KeyValuePair<string, double>("sadness", results[0]["scores"].Value<double>("sadness")));
				emotions.Add(new KeyValuePair<string, double>("surprise", results[0]["scores"].Value<double>("surprise")));

				var highest = emotions.OrderByDescending(kvp => kvp.Value).First().Key;

				Device.BeginInvokeOnMainThread(() =>
				{
					switch (highest)
					{
						case "anger":
							emoticonLabel.Text = "Mad \ud83d\ude21";
							break;

						case "contempt":
							emoticonLabel.Text = "Contempt ☺️";
							break;

						case "disgust":
							emoticonLabel.Text = "Disgusted \ud83d\ude12";
							break;

						case "fear":
							emoticonLabel.Text = "Feared \ud83d\ude28";
							break;

						case "happiness":
							emoticonLabel.Text = "Happy \ud83d\ude04";
							break;

						case "neutral":
							emoticonLabel.Text = "Neutral \ud83d\ude10";
							break;

						case "sadness":
							emoticonLabel.Text = "Sad \ud83d\ude22";
							break;

						case "surprise":
							emoticonLabel.Text = "Surprized \ud83d\ude2e";
							break;
					}
				});
			}

			else
			{
				Device.BeginInvokeOnMainThread(() =>
				{
					emoticonLabel.Text = "Unknown";
				});
			}
		}

		private async Task HandlePhoto(byte[] array)
		{
			var client = new HttpClient(new NativeMessageHandler());

			client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "cd21cc8207fd46828208c9b8172f5328");

			var content = new ByteArrayContent(array);

			content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
			var requestResult = await client.PostAsync("https://westus.api.cognitive.microsoft.com/face/v1.0/detect?returnFaceId=true&returnFaceLandmarks=false", content);

			if (!requestResult.IsSuccessStatusCode)
				return;

			var faces = JArray.Parse(await requestResult.Content.ReadAsStringAsync());

			var id = faces.FirstOrDefault()?.Value<string>("faceId");
			var personId = await FindPersonId(id);

			if (string.IsNullOrEmpty(personId))
				Device.BeginInvokeOnMainThread(() =>
				{
					nameLabel.Text = "Unknown";
					relationShipLabel.Text = "Unknown";
					emoticonLabel.Text = "Unknown";
					previewImage.IsVisible = false;
				});

			else
			{
				var person = await GetPerson(personId);

				if (person != null)
				{
					Device.BeginInvokeOnMainThread(() =>
					{
						nameLabel.Text = person.name;
						relationShipLabel.Text = person.userData;
						previewImage.IsVisible = person.name == "Shirley";
					});
				}

				else
					previewImage.IsVisible = false;
			}
		}

		bool isLoading = false;

		public RecognizerPage()
		{
			Title = "ReCognize";
			BindingContext = new RecognizerPageViewModel(Navigation);

			CameraView.OnPhotoResult += (result) =>
			{
				if (isLoading == false)
				{
					isLoading = true;
					Task.WhenAll(GetEmotions(result.Image), HandlePhoto(result.Image));
					isLoading = false;
				}
			};

			var grid = new Grid()
			{
				RowDefinitions = new RowDefinitionCollection()
				{
					new RowDefinition() { Height = GridLength.Star},
					new RowDefinition() { Height = GridLength.Auto}
				}
			};

			grid.Children.Add(CameraView, 0, 0);
			Grid.SetRowSpan(CameraView, 2);

			var timer = new System.Timers.Timer()
			{
				Interval = 2000,
				Enabled = true
			};

			int shirleyIndex = 1;

			timer.Elapsed += async (sender, e) =>
				{
					shirleyIndex++;

					await previewImage.FadeTo(0.5, 250);
					Device.BeginInvokeOnMainThread(() =>
						{
							previewImage.Source = "shirley" + shirleyIndex + ".jpg";
						});
					await previewImage.FadeTo(1, 250);

					if (shirleyIndex == 6)
						shirleyIndex = 1;
				};

			grid.Children.Add(new StackLayout()
			{
				Margin = 25,
				Padding = 10,
				BackgroundColor = Color.Black,
				Opacity = 0.5,
				Spacing = 2,
				Children =
				{
		new StackLayout()
		{
			Spacing = 0,
			Opacity = 1,
			Orientation = StackOrientation.Horizontal,
			Children =
						{
							new Label()
							{
								Text = "Name: ",
								TextColor = Color.White,
								FontAttributes = FontAttributes.Bold
							},
							nameLabel
						}
		},

					new StackLayout()
					{
						Spacing = 0,
						Opacity = 1,
						Orientation = StackOrientation.Horizontal,
						Children =
						{
							new Label()
							{
								Text = "Relationship: ",
								TextColor = Color.White,
								FontAttributes = FontAttributes.Bold
							},
							relationShipLabel
						}
					},

					new StackLayout()
					{
						Spacing = 0,
						Opacity = 1,
						Orientation = StackOrientation.Horizontal,
						Children =
						{
							new Label()
							{
								Text = "Emotion: ",
								TextColor = Color.White,
								FontAttributes = FontAttributes.Bold
							},
							emoticonLabel
						}
					},
					previewImage,
				}
			}, 0, 1);

			Content = grid;
		}

		public async Task<Person> GetPerson(string personId)
		{
			var client = new HttpClient(new NativeMessageHandler());

			client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "cd21cc8207fd46828208c9b8172f5328");

			var requestResult = await client.GetAsync("https://westus.api.cognitive.microsoft.com/face/v1.0/persongroups/hackathon/persons/" + personId);
			return JsonConvert.DeserializeObject<Person>(await requestResult.Content.ReadAsStringAsync());
		}

		public async Task<string> FindPersonId(string faceId)
		{
			var client = new HttpClient(new NativeMessageHandler());

			client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "cd21cc8207fd46828208c9b8172f5328");

			var json = new
			{
				personGroupId = "hackathon",
				faceIds = new[] { faceId },
				maxNumOfCandidatesReturned = 1,
				confidenceThreshold = 0.5
			};

			var requestResult = await client.PostAsync("https://westus.api.cognitive.microsoft.com/face/v1.0/identify", new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json"));

			if (!requestResult.IsSuccessStatusCode)
				return null;

			var result = JArray.Parse(await requestResult.Content.ReadAsStringAsync()).SingleOrDefault();

			if (result == null)
				return null;

			string personId = "";

			if (result["candidates"].Count() > 0)
				personId = result["candidates"][0].Value<string>("personId");

			return personId;
		}
	}
}
