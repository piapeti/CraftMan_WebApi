using CraftMan_WebApi.DataAccessLayer;
using CraftMan_WebApi.Models;
using Microsoft.Data.SqlClient;
 
using System.Data;

namespace CraftMan_WebApi.ExtendedModels
{
    public class Usermasterextended   
    {        
        public static Response RegistrationForJob(UserMaster _User, IConfiguration _configuration) 
        {
            try { Response strReturn = new Response();

                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("dbconn"));
                SqlDataAdapter sda = new SqlDataAdapter("select Password from dbo.tblUserMaster where upper(EmailId)=upper('" + _User.EmailId + "') or upper(Username)=upper('" + _User.Username + "')", con);

                DataTable da = new DataTable();
                sda.Fill(da);
                if (da.Rows.Count > 0)
                {
                    strReturn.StatusMessage = "User already exists...";
                    strReturn.StatusCode = 1;                    
                }
                else
                {
                    SqlCommand cmd = new SqlCommand(" INSERT into dbo.tblUserMaster(Username,Password,Active,LocationId,MobileNumber,ContactPerson,EmailId,CreatedOn)     VALUES('" + _User.Username + "','" + _User.Password + "','" + _User.Active + "','" + _User.LocationId + "','" + _User.MobileNumber + "','" + _User.ContactPerson + "','" + _User.EmailId + "',getdate())", con);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();

                    con.Close();
                    if (i > 0)
                    {
                        strReturn.StatusCode = 1;
                        strReturn.StatusMessage = "User Registered Successfully";
                    }
                    else
                    { strReturn.StatusMessage = "User not registered"; }
                }
                 
                return strReturn;
            }
            catch (Exception ex) { throw; }
        }
        public static Response LoginValidateForUser(LoginUser _User, IConfiguration _configuration) {
            try
            {

                Response strReturn = new Response(); 
                strReturn.StatusMessage= "Invalid User";
                strReturn.StatusCode = 1;
                DBAccess db = new DBAccess(_configuration);
                SqlDataReader sdr ;
                string queryString = "select Password from dbo.tblUserMaster where Password='" + _User.Password + "' and Active='" + _User.Active + "' and   upper(EmailId)=upper('" + _User.EmailId + "')  ";
                sdr = db.ReadDB(queryString);
                if (sdr.HasRows)
                {
                    strReturn.StatusMessage = "Valid User!";
                    strReturn.StatusCode = 1;                    
                } 
                sdr.Close ();
                return strReturn;
                

                }
            
            catch (Exception ex) { throw;  }
        }
    }
}
