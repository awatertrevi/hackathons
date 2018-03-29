//
// FoundItem.cs
//
// Trevi Awater (awatertrevi@gmail.com)
// 12/10/2016
//
using System;
using System.ComponentModel.DataAnnotations;

namespace FoundIt.Models
{
    public class FoundItem
    {
        [Key]
        public Guid Id { get; set; }

        public FoundItem()
        {
            Id = Guid.NewGuid();
            FoundTime = DateTime.Now;
        }

        public User Finder { get; set; }
        public LostItem LostItem { get; set; }

        public string Picture { get; set; }

        public string FindAddress { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public DateTime FoundTime { get; set; }
    }
}
