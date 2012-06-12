using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;
using System.Web.Configuration;
using System.Text;

namespace BigBoard.models
{
    public class LogData
    {
        private OracleConnection conn;

        public LogData() : this(WebConfigurationManager.AppSettings["connstring"]) { }

        public LogData(String connstring)
        {
            conn = new OracleConnection(connstring);
        }

        public List<ILogItem> DataSince(DateTime time)
        {
            OracleCommand comm = new OracleCommand(@"SELECT * FROM CSE.CSE_LOG 
                                                    WHERE ACTIVITY_DTIME > :time order by 1 desc", 
                                                    conn);
            comm.Parameters.Add(":time", OracleDbType.TimeStamp, 
                                    time, System.Data.ParameterDirection.Input);
            comm.Connection.Open();
            comm.Prepare();
            OracleDataReader rdr = comm.ExecuteReader();
            var output = BuildObjects(rdr);
            comm.Connection.Close();
            return output;
        }

        public List<ILogItem> DataSinceId(Int32 id)
        {
            OracleCommand comm = new OracleCommand(@"SELECT * FROM CSE.CSE_LOG 
                                                    WHERE LOG_ID > :id order by LOG_ID desc",
                                        conn);
            comm.Parameters.Add(":id", OracleDbType.Int32,
                                    id, System.Data.ParameterDirection.Input);
            comm.Connection.Open();
            comm.Prepare();
            OracleDataReader rdr = comm.ExecuteReader();
            var output = BuildObjects(rdr);
            comm.Connection.Close();
            return output;
        }

        private List<ILogItem> BuildObjects(OracleDataReader rdr){
            var output = new List<ILogItem>();
            while(rdr.Read())
            {
                CSELogItem temp = new CSELogItem();
                temp.ID = rdr[0].ToString();
                temp.UserID = rdr[1].ToString();
                temp.Date = rdr[2].ToString();
                temp.ProcedureName = rdr[3].ToString();
                temp.Message = rdr[4].ToString();
                output.Add(temp);
            }
            rdr.Dispose();
            return output;
        }

    }
}