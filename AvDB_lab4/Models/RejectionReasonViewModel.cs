using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AvDB_lab4.Entities.Credits;

namespace AvDB_lab4.Models
{
    public class RejectionReasonViewModel
    {
        public RejectionReasonViewModel() { }

        public RejectionReasonViewModel(RejectionReason? rejectionReason)
        {
            SelectedRejectionReason = rejectionReason;
            var availableRejectionReasonList = new List<object>();
            availableRejectionReasonList.Add(
                new { Value = (RejectionReason?)null, Text = "" } );
            foreach (var item in Enum.GetValues(typeof(RejectionReason)))
            {
                availableRejectionReasonList.Add(
                    new { Value = (int)item, Text = item.ToString() }
                    );
            }

            AvailableRejectionReasonList = new SelectList(
                availableRejectionReasonList,
                "Value",
                "Text");
        }

        public RejectionReason? SelectedRejectionReason { get; set; }
        public SelectList AvailableRejectionReasonList { get; set; }
    }
}