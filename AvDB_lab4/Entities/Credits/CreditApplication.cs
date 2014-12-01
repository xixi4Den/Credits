using System;
using AvDB_lab4.Entities.Clients;

namespace AvDB_lab4.Entities.Credits
{
    public class CreditApplication : BaseDbEntity
    {
        public DateTime RegisterDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        public Guid ClientId { get; set; }
        public virtual BaseClient Client { get; set; }
        public Guid CreditCategoryId { get; set; }
        public virtual CreditCategory CreditCategory { get; set; }
        public bool IsCompleted { get; set; }
        public Outcome? Outcome { get; set; }
        public RejectionReason? RejectionReason { get; set; }
    }
}