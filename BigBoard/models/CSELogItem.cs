using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BigBoard.models
{
    public class CSELogItem : ILogItem
    {
        public string ID { get; set; }
        public string UserID { get; set; }
        public string Date { get; set; }
        public string ProcedureName { get;  set; }
        public string Message { get; set; }
    }
}