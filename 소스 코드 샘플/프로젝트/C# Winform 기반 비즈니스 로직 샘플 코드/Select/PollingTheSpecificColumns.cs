using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;


namespace IMS.API.CLIENT.Business
{
    public class PollingTheSpecificColumns
    {
        public int Polling(string tableName)
        {
            string sql = " SELECT PROC_TYPE " +
                        $" FROM {tableName} " +
                         " WHERE UPPER(PROC_TYPE) = 'N'";

            DBConnector connector = new DBConnector();
            var connection = connector.Connection();

            try
            {
                OracleCommand command = new OracleCommand(sql, connection);
                command.ExecuteNonQuery();
                var oracleDataAdapter = new OracleDataAdapter(command);
                DataSet dataSet = new DataSet();
                oracleDataAdapter.Fill(dataSet);

                oracleDataAdapter.Dispose();
                connection.Close();
                return dataSet.Tables[0].Rows.Count;
            }
            catch
            {
                return -1;
            }
        }
    }
}
