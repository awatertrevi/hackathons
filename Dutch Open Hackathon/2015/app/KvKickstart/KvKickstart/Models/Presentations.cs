using System;
using System.Collections.Generic;
using System.Text;
using DOH2015.Framework;

namespace DOH2015.Models
{

	public class PresentationResponse
	{
		public bool success { get; set; }
		public string message { get; set; }
		public Body body { get; set; }
	}

	public class Body
	{
		public List<Presentation> presentations { get; set; }
	}

	public class Presentation
	{
		public int id { get; set; }
		public string title { get; set; }
		public string city { get; set; }
		public string time { get; set; }
		public string presenterId { get; set; }
		//public Presenter presenter { get { return KamerVanKoophandel.Presenter (int.Parse (presenterId)).Result; }}
		public Presenter presenter {get { return new Presenter() { name = "Lorem Ipsum" };}}
		public string building { get; set; }
		public string distance { get; set; }
		public string content { get; set; }
		public string imageUrl { get; set; }
	}
}
