using System.Data;
using System.Data.SqlClient;

namespace prjThiTracNghiem
{
    internal sealed class DataAccess
    {
        private SqlConnection connection;

        private DataAccess()
        {
            connection = new SqlConnection(DbConfig.ConnectionString);
        }

        #region Singleton Pattern 
        private static DataAccess objMSSQL = null;

        public static DataAccess Instance
        {
            get
            {
                if (objMSSQL == null)
                {
                    objMSSQL = new DataAccess();
                }
                return objMSSQL;
            }
        }
        #endregion

        #region Implemention IDataAccess

        public DataSet ExecuteQuery(string commandText, CommandType commandType = CommandType.Text, params SqlParameter[] parameters)
        {
            DataSet result = new DataSet();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = commandText;
                cmd.CommandType = commandType;
                cmd.Parameters.AddRange(parameters);

                connection.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(result);
                
                connection.Close();
            }
            return result;
        }

        #endregion
    }
}
