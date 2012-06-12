using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using BigBoard.models;
using System.Dynamic;

namespace BigBoard
{
    public class BigBoardModule : NancyModule
    {
        public BigBoardModule()
        {
            Get["/logs"] = parameters =>
            {
                LogData data = new LogData();
                dynamic model = new ExpandoObject();
                DateTime since = DateTime.Now.Add(new TimeSpan(-5, 0, 0));
                model.Logs = data.DataSince(since);
                return View["Logs",model];
            };

            Get["/"] = parameters => { return View["Index"]; };
        }
    }
}