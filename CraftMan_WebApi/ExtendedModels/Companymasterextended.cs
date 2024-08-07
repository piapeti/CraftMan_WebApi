using CraftMan_WebApi.DataAccessLayer;
using CraftMan_WebApi.Models;
using Microsoft.Data.SqlClient;
 
using System.Data;

namespace CraftMan_WebApi.ExtendedModels
{
    public class Companymasterextended   
    {
        public static Response RegistrationCompany(CompanyMaster _Company, IConfiguration _configuration)
        {
            try
            {
                Response strReturn = new Response();

                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("dbconn"));
                SqlDataAdapter sda = new SqlDataAdapter("select Password from dbo.tblCompanyMaster where upper(EmailId)=upper('" + _Company.EmailId + "') or upper(Username)=upper('" + _Company.Username + "')", con);

                DataTable da = new DataTable();
                sda.Fill(da);
                if (da.Rows.Count > 0)
                {
                    strReturn.StatusMessage = "Company already exists...";
                    strReturn.StatusCode = 1;
                }
                else
                {
                    SqlCommand cmd = new SqlCommand(" INSERT into dbo.tblCompanyMaster(Username,Password,Active,JobType,LocationId,MobileNumber,ContactPerson,EmailId,CreatedOn)     VALUES('" + _Company.Username + "','" + _Company.Password + "','" + _Company.Active + "','" + _Company.UserType + "','" + _Company.LocationId + "','" + _Company.MobileNumber + "','" + _Company.ContactPerson + "','" + _Company.EmailId + "',getdate())", con);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();

                    con.Close();
                    if (i > 0)
                    {
                        strReturn.StatusCode = 1;
                        strReturn.StatusMessage = "Comapny Registered Successfully";
                    }
                    else
                    { strReturn.StatusMessage = "Company not registered"; }
                }
                return strReturn;
            }
            catch (Exception ex) { throw; }
        }

         public static Response LoginValidateForCompany(LoginUser _User, IConfiguration _configuration) {
            try
            {

                Response strReturn = new Response(); 
                strReturn.StatusMessage= "Invalid Company";
                strReturn.StatusCode = 1;
                DBAccess db = new DBAccess(_configuration);
                SqlDataReader sdr ;
                string queryString = "select Password from dbo.tblCompanyMaster where Password='" + _User.Password + "' and Active='" + _User.Active + "' and   upper(EmailId)=upper('" + _User.EmailId + "')  ";
                sdr = db.ReadDB(queryString);
                if (sdr.HasRows)
                {
                    strReturn.StatusMessage = "Valid Company!";
                    strReturn.StatusCode = 1;                    
                } 
                sdr.Close ();
                return strReturn;
                

                }
            
            catch (Exception ex) { throw;  }
        }
    }
}
