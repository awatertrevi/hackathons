using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FlexWork.Models
{
    public class Rating
    {
        [ForeignKey("WorkspaceId")]
        public virtual Workspace Workspace { get; set; }
        public Guid WorkspaceId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public string UserId { get; set; }

        public int Stars { get; set; }

        public string Notice { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
