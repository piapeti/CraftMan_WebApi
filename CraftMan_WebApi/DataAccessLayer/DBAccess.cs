using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Data.SqlClient; 
using System.Data;
using CraftMan_WebApi.Models;
namespace CraftMan_WebApi.DataAccessLayer
{
    public class DBAccess
    {
        private String strConnection ;
        private SqlConnection strConnectionString;        
        private SqlCommand sqlCmd;
        private SqlTransaction sqlTransaction;



       // private SqlConnection _connection;
      //  private SqlTransaction _transaction;

        public void InitializeConnection(string connectionString)
        {
            strConnectionString = new SqlConnection(connectionString);
            strConnectionString.Open();
        }

        public void BeginTransaction()
        {
            sqlTransaction = strConnectionString.BeginTransaction();
        }

        public DBAccess()
        {

            var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
            strConnection = configuration.GetConnectionString("dbconn");


          //  strConnection = ConfigurationManager.AppSettings["DBConnectionString1"];
            strConnectionString = new SqlConnection(strConnection);  
            
        }
        
        public SqlDataReader  ReadDB(string SQLstr)
        {
            if (strConnectionString.State == ConnectionState.Closed)
                strConnectionString.Open();
            sqlCmd = strConnectionString.CreateCommand();
            sqlCmd.CommandTimeout = 6000;
            sqlCmd.CommandText = SQLstr;
            // ''---------------------
            if (!(this.sqlTransaction == null))
                // User has invoked a transaction. So add the Transaction to the command object
                this.sqlCmd.Transaction = this.sqlTransaction;
            // ''---------------------
            return sqlCmd.ExecuteReader();
        }
        public Response validate(string qstr)
        {
            Response strReturn = new Response();
            SqlConnection con = new SqlConnection(strConnection);
            SqlDataAdapter sda = new SqlDataAdapter(qstr, con);// "select JobType from [dbo].[tblJobTypeMaster]where upper(JobType)=upper('" + _Job.JobType + "') ", con);

            DataTable da = new DataTable();
            sda.Fill(da);

            if (da.Rows.Count > 0)
            {
                strReturn.StatusMessage = "Already exists...";
                strReturn.StatusCode = 1;

            }

            return strReturn;

        }

        public void AddParameter(string paramName, object paramValue, ParameterDirection paramDir = ParameterDirection.Input)
        {
            SqlParameter param = new SqlParameter(paramName, SqlDbType.VarChar)
            {
                Value = paramValue,
                Direction = paramDir,
                Size = 5000
            };

            sqlCmd.Parameters.Add(param);
        }

        public object ExecuteScalar(string commandText, CommandType cmdType = CommandType.StoredProcedure)
        {
            object obj = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    if (sqlTransaction != null)
                    {
                        // User has invoked a transaction. So add the Transaction to the command object
                        cmd.Transaction = sqlTransaction;
                    }
                    cmd.CommandType = cmdType;
                    cmd.CommandText = commandText;
                    cmd.Connection = strConnectionString;

                    obj = cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (log it, rethrow it, etc.)
                Logger.Error("Error executing scalar command", ex);
                throw;
            }
            return obj;
        }

        public int ExecuteNonQuery(string qstr) {
           
            if (strConnectionString.State == ConnectionState.Closed )
            {
                strConnectionString.Open();
            }
              sqlCmd = strConnectionString.CreateCommand();
            sqlCmd = new SqlCommand(qstr, strConnectionString);
           sqlCmd.CommandType = CommandType.Text; 
            return sqlCmd.ExecuteNonQuery();
        }
        
    }
}
