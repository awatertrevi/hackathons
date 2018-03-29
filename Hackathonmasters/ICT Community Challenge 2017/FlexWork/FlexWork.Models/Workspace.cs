using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FlexWork.Models
{
	public partial class Workspace
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		public string Name { get; set; }
		public string Description { get; set; }
		public string Address { get; set; }
		public string Postal { get; set; }
		public string City { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }

		public string CutOfDescription => Description != null ? Description.Length > 40 ? Description.Substring(0, 40) + "..." : Description : string.Empty;

		public List<WorkspaceFacility> WorkspaceFacilities { get; set; }
		public List<OpeningHours> OpeningHours { get; set; }

		public DateTimeOffset? PremiumTill { get; set; }

		public string Image { get; set; }

		public CompanyType CompanyType { get; set; }

		public bool IsPermanentlyClosed { get; set; }

		public string FullAddress => $"{Address}, {City}";
	}

	public enum CompanyType
	{
		Corporate,
		SmallBusiness
	}
}
