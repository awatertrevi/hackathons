//
// Configuration.cs
//
// Trevi Awater (t.awater@resoftware.nl)
// 11/18/2016
//

using System.Data.Entity.Migrations;
using System.Linq;
using FoundIt.Models;

namespace FoundIt.Webservice.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<LostItContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(LostItContext context)
        {
            //if (!context.WorkoutCategories.Any())
            //{
            //    context.WorkoutCategories.Add(new WorkoutCategory()
            //    {
            //        Name = "Core"
            //    });

            //    context.WorkoutCategories.Add(new WorkoutCategory()
            //    {
            //        Name = "Interval"
            //    });

            //    context.WorkoutCategories.Add(new WorkoutCategory()
            //    {
            //        Name = "Yoga"
            //    });

            //    context.WorkoutCategories.Add(new WorkoutCategory()
            //    {
            //        Name = "Crossfit"
            //    });

            //    context.WorkoutCategories.Add(new WorkoutCategory()
            //    {
            //        Name = "Boxing"
            //    });

            //    context.WorkoutCategories.Add(new WorkoutCategory()
            //    {
            //        Name = "Biken"
            //    });
            //    ;
            //    context.WorkoutCategories.Add(new WorkoutCategory()
            //    {
            //        Name = "Circuit"
            //    });

            //    context.WorkoutCategories.Add(new WorkoutCategory()
            //    {
            //        Name = "Sport"
            //    });

            //    context.WorkoutCategories.Add(new WorkoutCategory()
            //    {
            //        Name = "Personal"
            //    });
            //}
        }
    }
}