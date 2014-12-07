using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AvDB_lab4.Models
{
    public class ApprovalTaskViewModel
    {
        public TaskViewModel Task { get; set; }
        [Required]
        public Guid Id { get; set; }
        [Display(Name = "Outcome")]
        public OutcomeViewModel OutcomeViewModel { get; set; }
        [Display(Name = "Rejection Reason")]
        public RejectionReasonViewModel RejectionReasonViewModel { get; set; }
        public string UserId { get; set; }
        public IList<string> UserRoles { get; set; }
    }
}