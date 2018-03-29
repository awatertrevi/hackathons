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
using Xamarin.Forms;
using System.Threading.Tasks;
using What.ComponentModel;
using What.Models;
using AVFoundation;
using System.Linq;

namespace FoundIt.ViewModels
{
    public class RootPageViewModel : ObservableBase
    {
        public ObservableCollection<Message> Messages { get; }

        public ICommand SendCommand { get; set; }

        public List<Message> MockMessages;

        public RootPageViewModel()
        {
            Messages = new ObservableCollection<Message>();

            SendCommand = new Command((text) =>
            {
                var message = new Message
                {
                    Text = text as string,
                    Incomming = false,
                    MessageDateTime = DateTime.Now
                };

                Messages.Add(message);

                Device.BeginInvokeOnMainThread(() =>
                {
                    PrintResponse(message.Text);
                });
            });

            AddMessage(new Message()
            {
                Text = "How can I help you?",
                Incomming = true
            });
        }

        private void PrintResponse(string input)
        {
            if (input.ToLower().Contains("looking") || input.ToLower().Contains("towel"))
                AddMessage(new Message()
                {
                    Text = "There are 3 towels matching your search.\n\n2 at HEMA and 1 at the Bijenkorf.\n",
                    TowelQuestion = true,
                    Incomming = true,
                    Id = 0
                });

            else if (input.ToLower().Contains("purchase"))
                AddMessage(new Message()
                {
                    Text = "Please scan the product.",
                    BarcodeButton = true,
                    Incomming = true,
                    Id = 1
                });

            else if (input.ToLower().Contains("hello"))
                AddMessage(new Message()
                {
                    Text = "Hi!",
                    Incomming = true,
                    Id = 3
                });

            else if (input.ToLower().Contains("who are you"))
                AddMessage(new Message()
                {
                    Text = "I am iBotIt, here to help customers find their product fast.\n\n I am built on the Microsoft Bot Framework. The client you are using now is a native iOS application made with Xamarin.\n\nI currently use mock data from HEMA because the real API isn't available just yet.\n\nIn the future I will be able to talk to you everywhere. For example Siri or Amazon Alexa.",
                    Incomming = true,
                    Id = 4
                });

            else
                AddMessage(new Message()
                {
                    Text = "I'm sorry, I don't understand. Should I contact an employee?",
                    Incomming = true,
                    YesNoQuestion = true,
                    Id = 2
                });
        }

        public void AddMessage(Message message)
        {
            message.MessageDateTime = DateTime.Now;

            Messages.Add(message);

            Device.BeginInvokeOnMainThread(() =>
            {
                var speechSynthesizer = new AVSpeechSynthesizer();
                var speechUtterance = new AVSpeechUtterance(message.Text)
                {
                    Rate = AVSpeechUtterance.DefaultSpeechRate,
                    Voice = AVSpeechSynthesisVoice.FromLanguage("en-EN"),
                    Volume = 0.5f,
                    PitchMultiplier = 1.0f
                };

                speechSynthesizer.SpeakUtterance(speechUtterance);

                MessagingCenter.Send(this, "ScrollDown");
            });
        }
    }
}