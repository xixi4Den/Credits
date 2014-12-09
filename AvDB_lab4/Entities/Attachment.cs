using System;
using AvDB_lab4.Entities.Credits;

namespace AvDB_lab4.Entities
{
    public class Attachment: BaseDbEntity
    {
        public string Source { get; set; }
        public Guid ApplicationId { get; set; }
        public bool IsContract { get; set; }

        public virtual CreditApplication Application { get; set; }
    }
}