//
// LostItem.cs
//
// Trevi Awater (awatertrevi@gmail.com)
// 12/9/2016
//
using System;
using System.ComponentModel.DataAnnotations;

namespace FoundIt.Models
{
    public class LostItem
    {
        public LostItem()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public User Reporter { get; set; }

        public string LoseAddress { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public double? Radius { get; set; }

        public bool? IsStolen { get; set; }
        
        public DateTime? EstimatedLoseTime { get; set; }
    }
}