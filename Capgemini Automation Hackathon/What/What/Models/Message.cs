using System;
namespace What.Models
{
    public class Message
    {
        public string Text { get; set; }

        public bool Incomming { get; set; }

        public DateTime MessageDateTime { get; set; }

        public bool BarcodeButton { get; set; }

        public bool YesNoQuestion { get; set; }

        public bool TowelQuestion { get; set; }

        public bool FloorPlan { get; set; }

        public int Id { get; set; }
    }
}
