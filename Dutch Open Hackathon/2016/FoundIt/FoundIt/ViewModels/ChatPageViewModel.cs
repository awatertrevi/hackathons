//
// ChatPageViewModel.cs
//
// Trevi Awater (awatertrevi@gmail.com)
// 12/10/2016
//
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using FoundIt.ComponentModel;
using MonkeyChat;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Threading;
using FoundIt.Utilities.Helpers;
using FoundIt.Views.Pages;
using FoundIt.Models;

namespace FoundIt.ViewModels
{
    public class ChatPageViewModel : ViewModelBase
    {
        public ObservableCollection<Message> Messages { get; }

        public string OutGoingText { get { return get(() => this.OutGoingText); } set { set(() => this.OutGoingText, value); } }

        public FoundItem FItem { get { return get(() => this.FItem); } set { set(() => this.FItem, value); } }

        public ICommand SendCommand { get; set; }
        public ICommand GetBarcode { get; set; }

        public List<Message> MockMessages;

        private async Task fancyLoop()
        {
            MockMessages = new List<Message>()
            {
                new Message() { Text = "Hi, Nick.", Incomming = false },
                new Message() { Text = $"Klopt het dat jij mijn {FItem.LostItem.Name} hebt gevonden?", Incomming = false },
                new Message() { Text = $"Ja, dat klopt. Hij lag op een bankje bij {FItem.FindAddress}.", Incomming = true },
                new Message() { Text = "Ok, super!", Incomming = false },
                new Message() { Text = "Zou je hem op kunnen sturen?", Incomming = false },
                new Message() { Text = "Jazeker ga ik meteen doen! Naar welk adres zal ik hem versturen?", Incomming = true },
                new Message() { Text = "Sluiskamp 3023, Wijchen.", Incomming = false },
                new Message() { Text = "Ok ga ik doen!", Incomming = true },
                new Message() { Text = "Zodra ik hem binnen heb geef ik de vindersloon. Bedankt voor de moeite \ud83d\udc4d\ud83c\udffb!", Incomming = false }
            };

            foreach (var message in MockMessages)
            {
                Messages.Add(message);

                message.MessageDateTime = DateTime.Now;

                await Task.Delay(message.Text.Length * 100);
            }
        }

        public ChatPageViewModel(INavigation navigation, FoundItem item) : base(navigation)
        {
            Messages = new ObservableCollection<Message>();

            FItem = item;

            fancyLoop();

            SendCommand = new Command(() =>
            {
                var message = new Message
                {
                    Text = OutGoingText,
                    Incomming = false,
                    MessageDateTime = DateTime.Now
                };

                Messages.Add(message);

                OutGoingText = string.Empty;
            });

            GetBarcode = new Command(async () =>
            {
                await NavigationHandler.PushAsync(Navigation, new BarCodePage());
            });
        }
    }
}