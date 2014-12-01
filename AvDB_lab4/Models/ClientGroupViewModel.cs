using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AvDB_lab4.Entities.Clients;

namespace AvDB_lab4.Models
{
    public class ClientGroupViewModel
    {
        public ClientGroupViewModel()
        {
            
        }

        public ClientGroupViewModel(ClientGroup group)
        {
            SelectedClientGroup = group;
            var availableClientGroupList = new List<object>();
            foreach (var clientGroup in Enum.GetValues(typeof(ClientGroup)))
            {
                availableClientGroupList.Add(
                    new {Value = (int) clientGroup, Text = clientGroup.ToString()}
                    );
            }

            AvailableClientGroupList = new SelectList(
                availableClientGroupList,
                "Value",
                "Text");
        }

        public ClientGroup SelectedClientGroup { get; set; }
        public SelectList AvailableClientGroupList { get; set; }
    }
}