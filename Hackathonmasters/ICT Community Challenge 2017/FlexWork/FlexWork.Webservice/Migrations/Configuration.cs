//
// Configuration.cs
//
// Trevi Awater (t.awater@resoftware.nl)
// 11/18/2016
//

using FlexWork.Models;

namespace FlexWork.Migrations
{
	using System;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;

	internal sealed class Configuration : DbMigrationsConfiguration<FlexWorkContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = true;
			AutomaticMigrationDataLossAllowed = true;
		}

		protected override void Seed(FlexWorkContext context)
		{
			if (!context.Facilities.Any())
			{
				context.Facilities.Add(new Facility()
				{
					Name = "Wi-Fi"
				});

				context.Facilities.Add(new Facility()
				{
					Name = "Toilet"
				});

				context.Facilities.Add(new Facility()
				{
					Name = "Stroom"
				});

				context.Facilities.Add(new Facility()
				{
					Name = "Coffee"
				});

				context.Facilities.Add(new Facility()
				{
					Name = "Food"
				});

				context.Facilities.Add(new Facility()
				{
					Name = "Meeting Room"
				});

                context.Facilities.Add(new Facility()
                {
                    Name = "Parking"
                });
            }
		}
	}
}
