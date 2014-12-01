using System;
using System.Web.Mvc;

namespace AvDB_lab4.Models
{
    public class CreditCategoryViewModel
    {
        public Guid SelectedCreditCategoryId { get; set; }
        public SelectList AvailableCreditCategoryList { get; set; }
    }
}