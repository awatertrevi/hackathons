using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FlexWork.Models
{
    public class OpeningHours
    {
        [ForeignKey("WorkspaceId")]
        public Workspace Workspace { get; set; }
        public Guid WorkspaceId { get; set; }

        public DayOfWeek Day { get; set; }
        public bool IsClosed { get; set; }
        public TimeSpan? OpenTime { get; set; }
        public TimeSpan? CloseTime { get; set; }
    }
}
