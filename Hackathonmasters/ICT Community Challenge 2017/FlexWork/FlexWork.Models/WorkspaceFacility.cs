using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FlexWork.Models
{
    public class WorkspaceFacility
    {
        [ForeignKey("WorkspaceId")]
        public virtual Workspace Workspace { get; set; }
        public Guid WorkspaceId { get; set; }

        [ForeignKey("FacilityId")]
        public virtual Facility Facility { get; set; }
        public Guid FacilityId { get; set; }
    }
}
