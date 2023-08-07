using System;

namespace IMS.API.CLIENT.Business
{
    public class Update
    {
        private int ExecuteNonQueryWithReturnAffectedRows(string sql)
        {          
            try {
                var connector = new DBConnector();
                return connector.ExecuteNonQueryWithReturnAffectedRows(sql);
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public int UpdateProcTypeWithReturnAffectedRows(string tableName, string procType)
        {
            string sql = $"UPDATE {tableName} " +
                          "SET " +
                         $"PROC_TYPE = '{procType}' " +
                          "WHERE UPPER(PROC_TYPE) = 'N'";

            return ExecuteNonQueryWithReturnAffectedRows(sql);
        }

        public int Update_RMK_IN_MC_TSK_STAT()
        {
            string sql = "UPDATE MC_TSK_STAT " +
                         "SET UPD_DTTM = SYSDATE " +
                         "WHERE TASK_NO = 'IF_WMS_S'";

            return ExecuteNonQueryWithReturnAffectedRows(sql);
        }
    }
}
