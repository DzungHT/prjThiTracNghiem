using prjThiTracNghiem.Models;
using System.Collections.Generic;
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
        public int ExcuteNonQuery(string commandText,CommandType commandType,params SqlParameter[] parameters)
        {
            try
            {
                using(var cmd= new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = commandText;
                    cmd.CommandType = commandType;
                    cmd.Parameters.AddRange(parameters);
                    connection.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (System.Exception)
            {
                return 0;
            }
            finally
            {
                connection.Close();
            }
        }
        public int InsertBaithi(int _DethiID,int _SinhvienID,string date)
        {
            try
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "sp_Baithi_Insert";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@DethiID", _DethiID));
                    cmd.Parameters.Add(new SqlParameter("@SinhvienID", _SinhvienID));
                    cmd.Parameters.Add(new SqlParameter("@Ngaylambai", date));
                    SqlParameter outPutId = new SqlParameter();
                    outPutId.ParameterName = "@id";
                    outPutId.DbType = DbType.Int64;
                    outPutId.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outPutId);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    return int.Parse(cmd.Parameters["@id"].Value.ToString());
                }
            }
            catch (System.Exception)
            {
                return 0;
            }
            finally
            {
                connection.Close();
            }
        }
        public bool InsertCauhoiBaithi(List<CauHoi> lst,int id) 
        {
            try
            {
                using (var cmd = new SqlCommand())
                {
                    SqlTransaction tran = connection.BeginTransaction();
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.CommandText = "sp_Cauhoibaithi_Insert";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Transaction = tran;
                    for(int i = 0; i < lst.Count; i++)
                    {
                        cmd.Parameters.Add(new SqlParameter("@BaithiID", id));
                        cmd.Parameters.Add(new SqlParameter("@CauhoiID", lst[i].CauHoiID));
                        cmd.Parameters.Add(new SqlParameter("@Thutu", i+1));
                        cmd.ExecuteNonQuery();
                    }
                    tran.Commit();
                }
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
        #endregion
    }
}
