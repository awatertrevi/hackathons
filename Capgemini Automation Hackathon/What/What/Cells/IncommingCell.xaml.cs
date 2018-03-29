using System;
using System.Collections.Generic;

using Xamarin.Forms;
using What.Models;

namespace What.Cells
{
    public partial class IncommingCell : ViewCell
    {
        public IncommingCell()
        {
            InitializeComponent();

            var colors = new string[] { "#73cfca", "#e6d025", "#ffaa01", "#ff6f3d", "#e8204e" };
            var rand = new Random();

            frame.BackgroundColor = Color.FromHex(colors[rand.Next(colors.Length)]);
        }

        void BarCodeScanner(object sender, System.EventArgs e)
        {
            MessagingCenter.Send(this, "OpenScanner");
        }

        void SendYes(object sender, System.EventArgs e)
        {
            Message data = (Message)BindingContext;

            MessagingCenter.Send(this, "SendYes", data.Id);
        }

        void Handle_Tapped(object sender, System.EventArgs e)
        {
            MessagingCenter.Send(this, "SendNavigationMessage");
        }

        void SendNo(object sender, System.EventArgs e)
        {
            MessagingCenter.Send(this, "SendNo");
        }
    }
}
