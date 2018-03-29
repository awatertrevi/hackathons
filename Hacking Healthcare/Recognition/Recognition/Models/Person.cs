using System;
using System.Collections.Generic;
using Realms;

namespace Recognition.Models
{
	public class Person
	{
		public string personId { get; set; }
		public List<string> persistedFaceIds { get; set; }
		public string name { get; set; }
		public string userData { get; set; }
	}
}
