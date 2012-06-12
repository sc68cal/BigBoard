using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BigBoard.models
{
    public interface ILogItem
    {
        string ID { get; }
        string Message {get;}
        string Date{get;}
    }
}