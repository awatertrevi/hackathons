//
// ShipmentLocation.cs
//
// Trevi Awater (awatertrevi@gmail.com)
// 12/11/2016
//
using System;
namespace FoundIt.Models
{
    public class ShipmentLocation
    {
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Street { get; set; }
        public string HouseNr { get; set; }
        public string City { get; set; }
    }
}
