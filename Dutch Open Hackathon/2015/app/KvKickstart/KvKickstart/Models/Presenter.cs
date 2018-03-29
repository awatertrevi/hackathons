using System;

namespace DOH2015
{
	public class PresenterResponse
	{
		public bool success { get; set; }
		public string message { get; set; }
		public Presenter body { get; set; }
	}

	public class Presenter
	{
		public string id { get; set; }
		public string name { get; set; }
		public string company { get; set; }
		public string avatar { get; set; }
		public string email { get; set; }
		public string twitter { get; set; }
		public string linkedIn { get; set; }
	}
}
