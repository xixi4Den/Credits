using System;
using AvDB_lab4.Entities.Clients;
using AvDB_lab4.Entities.Credits;

namespace AvDB_lab4.Models
{
    public class ApplicationViewModel
    {
        public DateTime RegisterDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        public Guid ClientId { get; set; }
        public BaseClient Client { get; set; }
        public Guid CreditCategoryId { get; set; }
        public CreditCategory CreditCategory { get; set; }
        public bool IsCompleted { get; set; }
        public Outcome? Outcome { get; set; }
        public RejectionReason? RejectionReason { get; set; }
    }
}