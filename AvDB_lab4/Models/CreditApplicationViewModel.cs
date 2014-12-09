using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AvDB_lab4.Entities;
using AvDB_lab4.Entities.Credits;

namespace AvDB_lab4.Models
{
    public class CreditApplicationViewModel
    {
        public Guid Id { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        public Guid ClientId { get; set; }
        public bool IsCompleted { get; set; }
        public Outcome? Outcome { get; set; }
        public RejectionReason? RejectionReason { get; set; }

        [Display(Name = "Client Group")]
        public ClientGroupViewModel ClientGroupViewModel { get; set; }

        [Display(Name = "Credit Category")]
        public CreditCategoryViewModel CreditCategoryViewModel { get; set; }
    }
}