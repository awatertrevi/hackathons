using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FlexWork.Models
{
    public class WorkspaceRegistration
    {
        public WorkspaceRegistration()
        {
            CheckInDateTime = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public string UserId { get; set; }

        [ForeignKey("WorkspaceId")]
        public virtual Workspace Workspace { get; set; }
        public Guid WorkspaceId { get; set; }

        public DateTime CheckInDateTime { get; set; }
    }
}
