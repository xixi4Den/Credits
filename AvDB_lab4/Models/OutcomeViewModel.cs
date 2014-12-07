using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using AvDB_lab4.Entities.Credits;

namespace AvDB_lab4.Models
{
    public class OutcomeViewModel
    {
        public OutcomeViewModel() { }

        public OutcomeViewModel(Outcome? outcome)
        {
            SelectedOutcome = outcome;
            var availableOutcomeList = new List<object>();
            availableOutcomeList.Add(
                new { Value = (Outcome?)null, Text = ""});
            foreach (var item in Enum.GetValues(typeof(Outcome)))
            {
                availableOutcomeList.Add(
                    new { Value = (int)item, Text = item.ToString() }
                    );
            }

            AvailableOutcomeList = new SelectList(
                availableOutcomeList,
                "Value",
                "Text");
        }

        [Required]
        public Outcome? SelectedOutcome { get; set; }
        public SelectList AvailableOutcomeList { get; set; }
    }
}