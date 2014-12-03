using AvDB_lab4.Entities.Credits;
using AvDB_lab4.Entities.Credits.Tasks.Approvals;

namespace AvDB_lab4.Models
{
    public class ApprovalTaskViewModel
    {
        public TaskViewModel Task { get; set; }
        public ApprovalType ApprovalType { get; set; }
        public Outcome? Outcome { get; set; }
        public RejectionReason? RejectionReason { get; set; }
    }
}