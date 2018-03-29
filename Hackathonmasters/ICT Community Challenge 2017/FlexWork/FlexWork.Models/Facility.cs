using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FlexWork.Models
{
	public class Facility
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		public string Name { get; set; }

		public string Icon => Name.ToLower() + ".png";

		public string SmallIcon => Name.ToLower() + "_small.png";
	}
}
