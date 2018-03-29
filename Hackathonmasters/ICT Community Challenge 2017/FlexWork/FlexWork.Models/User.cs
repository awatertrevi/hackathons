using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace FlexWork.Models
{
	public partial class User
	{
		public User()
		{
			Created = DateTime.Today;
		}

		public string FirstName { get; set; }

		public string MiddleName { get; set; }

		public string LastName { get; set; }

		public string Gender { get; set; }

		public string ProfilePhoto { get; set; }

		[JsonIgnore]
		public long? FacebookId { get; set; }

		[JsonIgnore]
		[Column(TypeName = "date")]
		public DateTime Created { get; set; }

		[JsonIgnore]
		public bool iOS { get; set; }

		[JsonIgnore]
		public bool Android { get; set; }

		[JsonIgnore]
		public string FullName => FirstName + " " + (MiddleName ?? LastName) + (MiddleName != null ? " " + LastName : string.Empty);

		public string Bio { get; set; }
		public string Company { get; set; }
		public Expertise ExpertiseGroup { get; set; }


		public enum Expertise
		{
			Consultancy,
			Management,
			Accounting,
			Banking,
			Coaching,
			IT,
			Design,
			Logistics,
			LegalAdmin,
			Marketing,
			Education,
			Government,
			Sales,
			Journalism,
			HR
		}
	}

}
