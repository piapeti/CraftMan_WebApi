using CraftMan_WebApi.Controllers;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
namespace CraftMan_WebApi.DataAccessLayer
{
    public class DBAccess
    {
     
        private SqlConnection strConnectionString;
        
        private SqlCommand sqlCmd;
        private SqlTransaction sqlTransaction;

        public DBAccess(IConfiguration _config)
        {             
            strConnectionString = new SqlConnection(_config.GetConnectionString("dbconn")); // _configuration.GetConnectionString("dbconn")); // "Provider=SQLOLEDB.1;Initial Catalog=CFSTEST;Data Source=server;user id=sa;password=admin@123"

            sqlCmd = new SqlCommand();
            sqlCmd.Connection = strConnectionString;
            sqlCmd.CommandType = CommandType.Text;
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

    }
}
