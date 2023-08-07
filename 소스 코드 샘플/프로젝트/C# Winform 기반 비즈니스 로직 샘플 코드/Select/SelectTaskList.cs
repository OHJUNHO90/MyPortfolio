using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;

namespace IMS.API.CLIENT.Business
{
    internal struct MC_TSK_STAT
    {
        public string TASK_NO;
        public string TASK_TYPE;
        public string IP;
        public string PORT;
        public int MILLISECONDS;
    }

    internal class SelectTaskList
    {
        public List<MC_TSK_STAT> list { private set; get; } = new List<MC_TSK_STAT>();

        private const string sql = "SELECT  TASK_NO, " +
                                           "TASK_TYPE, " +
                                           "IP, " +
                                           "PORT, " +
                                           "MILLISECONDS " +
                                   "FROM MC_TSK_STAT " +
                                   "WHERE TASK_TYPE = 'APOLLO-WM-S'";

        public List<MC_TSK_STAT> GetTaskListFromDB()
        {
            string tableName = "settingInfo";
            OracleDataAdapter oracleDataAdapter = new OracleDataAdapter( new DBConnector().
                                                                         ExecuteNonQueryWithReturnOracleCommand(sql) );
            DataSet dataSet = new DataSet();
            oracleDataAdapter.Fill(dataSet, tableName);

            for (int i = 0; i < dataSet.Tables[tableName].Rows.Count; i++)
            {
                var rowInfo          = new MC_TSK_STAT();
                rowInfo.TASK_NO      = dataSet.Tables[tableName].Rows[i]["TASK_NO"] as string;
                rowInfo.TASK_TYPE    = dataSet.Tables[tableName].Rows[i]["TASK_TYPE"] as string;
                rowInfo.IP           = dataSet.Tables[tableName].Rows[i]["IP"] as string;
                rowInfo.PORT         = dataSet.Tables[tableName].Rows[i]["PORT"] as string;
                rowInfo.MILLISECONDS = Convert.ToInt32(dataSet.Tables[tableName].Rows[i]["MILLISECONDS"]);

                list.Add(rowInfo);
            }

            oracleDataAdapter.Dispose();

            return list;
        }
    }   
}
