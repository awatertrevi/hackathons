//
// User.cs
//
// Trevi Awater (awatertrevi@gmail.com)
// 12/9/2016
//
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FoundIt.Models
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
        public string Fullname => FirstName + " " + (MiddleName ?? LastName) + (MiddleName != null ? " " + LastName : string.Empty);


        [JsonIgnore]
        public ICollection<LostItem> LostItems { get; set; }
        [JsonIgnore]
        public ICollection<FoundItem> FoundItems { get; set; }
    }
}
