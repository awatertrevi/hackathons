using System;
using System.Collections.Generic;
using System.Text;

namespace DOH2015.Models
{
    public class CitiesResponse
    {
		public CitiesResponse() {}
        public bool success { get; set; }
        public string message { get; set; }
        public CityBody body { get; set; }
    }

    public class CityBody
    {
        public List<City> cities { get; set; }
    }

    public class City
    {
		public City()
		{
		}

        public string city { get; set; }
    }

}
