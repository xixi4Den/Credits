using System.Collections.Generic;
using AvDB_lab4.Entities.Credits.Tasks.Approvals;

namespace AvDB_lab4.Business
{
    public static class TaskTypeRolesMapping
    {
        public static readonly IDictionary<ApprovalType, string> Values = new Dictionary
            <ApprovalType, string>
        {
            {ApprovalType.CreditCommittee, "CreditCommitteeEmployee"},
            {ApprovalType.RiskManagment, "RiskManager"},
            {ApprovalType.SecurityService, "SecurityManager"},
            {ApprovalType.Fianl, "AuthorizedParty"},
        };
    }
}