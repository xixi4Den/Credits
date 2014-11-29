using System;

namespace AvDB_lab4.Entities.Credits.Tasks.Approvals
{
    public class ApprovalTask : BaseTask
    {
        public ApprovalType ApprovalType { get; set; }
        public Outcome? Outcome { get; set; }
        public RejectionReason? RejectionReason { get; set; }
    }
}