using System;
using System.ComponentModel.DataAnnotations;
using AvDB_lab4.Entities.Credits.Tasks;

namespace AvDB_lab4.Models
{
    public class TaskViewModel
    {
        public Guid Id { get; set; }
        [Display(Name = "Client")]
        public string ClientName { get; set; }
        [Display(Name = "Task")]
        public string DispalyText { get; set; }
        [Display(Name = "Credit Category")]
        public string CreditCategoryName { get; set; }
        public TaskStatus Status { get; set; }
        [Display(Name = "Created")]
        public DateTime CreateDate { get; set; }
        [Display(Name = "Completed")]
        [DisplayFormat(NullDisplayText = "-")]
        public DateTime? CompleteDate { get; set; }       
        [Display(Name = "Assigned to")]
        [DisplayFormat(NullDisplayText = "-")]
        public string AssignedTo { get; set; }
        public Guid ClientId { get; set; }
        public string UserId { get; set; }
    }
}