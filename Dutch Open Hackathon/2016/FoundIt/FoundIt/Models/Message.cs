using System;
using FoundIt.ComponentModel;
namespace MonkeyChat
{
    public class Message : ObservableBase
    {
        public string Text { get { return get(() => this.Text); } set { set(() => this.Text, value); } }

        public DateTime MessageDateTime { get { return get(() => this.MessageDateTime); } set { set(() => this.MessageDateTime, value); } }

        public string MessageTimeDisplay => MessageDateTime.ToString();

        public bool Incomming { get { return get(() => this.Incomming); } set { set(() => this.Incomming, value); } }
    }
}

