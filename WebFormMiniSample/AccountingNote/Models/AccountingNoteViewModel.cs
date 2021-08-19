using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccountingNote.Models
{
    public class AccountingNoteViewModel
    {
        public string ID { get; set; }
        public string Caption { get; set; }
        public int Amount { get; set; }
        public string ActType { get; set; }
        public string CreateDate { get; set; }
        public string Body { get; set; }
    }
}