using System;
using Realms;
namespace Recognition.Models
{
	public class SavedPersonImage : RealmObject
	{
		public string personId { get; set; }
		public string image { get; set; }
	}
}
