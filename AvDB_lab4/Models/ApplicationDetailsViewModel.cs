using AvDB_lab4.Entities.Credits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AvDB_lab4.Models
{
    public class ApplicationDetailsViewModel
    {
        public Guid Id { get; set; }
        public string ClientName { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        public virtual CreditCategory CreditCategory { get; set; }
        public bool IsCompleted { get; set; }
        public Outcome? Outcome { get; set; }
        public RejectionReason? RejectionReason { get; set; }
    }
}