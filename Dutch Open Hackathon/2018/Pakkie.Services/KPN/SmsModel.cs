using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using Newtonsoft.Json;

namespace Pakkie.Services.KPN
{
    public class SmsModel
    {
        [JsonProperty("messages")]
        public Message[] Messages { get; set; }
        [JsonProperty("sender")]
        public string Sender { get; set; }

        public SmsModel()
        {

        }
    }

    public class Message
    {
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("mobile_number")]
        public string MobileNumber { get; set; }

        public Message()
        {

        }

        public Message(string content, string mobilenumber)
        {
            Content = content;
            MobileNumber = mobilenumber;
        }
    }
}
