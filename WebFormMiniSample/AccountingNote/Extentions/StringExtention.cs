using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccountingNote.Extentions
{
    public static class StringExtention
    {
        public static Guid ToGuid(this string guidText)
        {
            if (Guid.TryParse(guidText, out Guid tempGuid))
                return tempGuid;
            else
                return Guid.Empty;
        }
    }
}