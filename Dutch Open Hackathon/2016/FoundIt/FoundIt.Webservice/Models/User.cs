//
// User.cs
//
// Trevi Awater (t.awater@resoftware.nl)
// 11/18/2016
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoundIt.Models
{
    public partial class User : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            return userIdentity;
        }

        public override string Id
        {
            get { return base.Id; }
            set { base.Id = value; }
        }

        public override string UserName
        {
            get { return base.UserName; }
            set { base.UserName = value; }
        }
    }

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
        public string Platform { get; set; }
       

        [JsonIgnore]
        public string Fullname => FirstName + " " + (MiddleName ?? LastName) + (MiddleName != null ? " " + LastName : string.Empty);
    }
}