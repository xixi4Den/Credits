using System;
using AvDB_lab4.Models;

namespace AvDB_lab4.Entities.Credits.Tasks
{
    public abstract class BaseTask : BaseDbEntity
    {
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public Guid CreditApplicationId { get; set; }
        public virtual CreditApplication CreditApplication { get; set; }
        public string DispalyText { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? CompleteDate { get; set; }
    }
}