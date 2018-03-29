using DOH2015.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace DOH2015.Framework
{
    public static class KamerVanKoophandel
    {
        static RestClient client = new RestClient("http://hackathon.modision.com/api/");

        public static Task<T> ExecuteAsync<T>(RestRequest request) where T : new()
        {
            var taskCompletionSource = new TaskCompletionSource<T>();
			client.ExecuteAsync<T> (request, (response) => taskCompletionSource.SetResult (response.Data));
            return taskCompletionSource.Task;
        }

        public static Task<List<City>> Cities()
        {
        	var request = new RestRequest("cities");
        	var response = client.Execute<CitiesResponse>(request);
			return Task.FromResult (response.Data.body.cities);
        }

        public static Task<List<Presentation>> Presentations(string city)
        {
			var request = new RestRequest ("presentations");
			request.AddParameter ("city", city);
			var response = client.Execute<PresentationResponse> (request);
			return Task.FromResult (response.Data.body.presentations);
        }

		public static Task<Presenter> Presenter(int id)
		{
			var request = new RestRequest ("presentors");
			request.AddParameter ("id", id);
			var response = client.Execute<PresenterResponse> (request);
			return Task.FromResult (response.Data.body);
		}

		public static City City(string cityName)
		{
			var request = new RestRequest ("cities");
			var response = client.Execute<CitiesResponse> (request);
			return response != null ? response.Data.body.cities.FirstOrDefault (x => x.city == cityName) : null;
		}

		public static List<Presentation> Presentations(string city, List<int> presentationIds)
		{
			return Presentations (city).Result.Where (x => presentationIds.Contains (x.id)).ToList();
		}
    }
}
