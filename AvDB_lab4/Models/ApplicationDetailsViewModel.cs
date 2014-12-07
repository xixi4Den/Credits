using System.ComponentModel.DataAnnotations;
using AvDB_lab4.Entities.Credits;
using System;

namespace AvDB_lab4.Models
{
    public class ApplicationDetailsViewModel
    {
        public Guid Id { get; set; }
        [Display(Name="Client")]
        public string ClientName { get; set; }
        [Display(Name = "Register Date")]
        public DateTime RegisterDate { get; set; }
        [Display(Name = "Complete Date")]
        public DateTime? CompleteDate { get; set; }
        [Display(Name = "Credit Category")]
        public virtual CreditCategory CreditCategory { get; set; }
        public bool IsCompleted { get; set; }
        public Outcome? Outcome { get; set; }
        [Display(Name = "Rejection Reason")]
        public RejectionReason? RejectionReason { get; set; }
    }
}