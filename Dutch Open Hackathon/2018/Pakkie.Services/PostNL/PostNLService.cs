using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pakkie.Services.PostNL.Pickup;
using Pakkie.Services.PostNL.ShippingStatus;

namespace Pakkie.Services.PostNL
{
    public class PostNLService
    {
        private string apikey = "GAypxOjG1jG3lJENewTbxWC7aZnBMmJV";
        //private readonly TimeframeClient timeframeClient = new TimeframeClient();
        private readonly ShippingStatusClient shippingStatusClient = new ShippingStatusClient();
        private readonly PickupClient pickupClient = new PickupClient();

        //public async Task GetTimeframes()
        //{
            
        //}

        public async Task<Response> GetShipmentStatus(string barcode)
        {
            return await shippingStatusClient.BarcodeAsync(apikey, barcode, false);
        }

        public async Task<ObservableCollection<Location>> GetNearestPickupLocations(string postalCode, string city, string street, int? houseNumber)
        {
            return await pickupClient.NearestAsync(apikey, CountryCode.NL, postalCode, city, street, houseNumber, DateTime.Now.ToString("O"),
                string.Empty, new List<Anonymous>());
        }
    }
}
