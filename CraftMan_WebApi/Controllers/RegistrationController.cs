using CraftMan_WebApi.Models;
using CraftMan_WebApi.ExtendedModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
 
using System.Data;
 

namespace CraftMan_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public RegistrationController(IConfiguration _config) { _configuration = _config; }
        [HttpPost]
        [Route("RegisterNewPerson")]
        public Response RegisterUser(UserMaster _User)
        {

            return Usermasterextended.RegistrationForJob(_User, _configuration);
            //SqlConnection con = new SqlConnection(_configuration.GetConnectionString("dbconn"));
            //SqlDataAdapter sda = new SqlDataAdapter("select Password from dbo.tblUserMaster where upper(EmailId)=upper('" + _User.EmailId + "') or upper(Username)=upper('" + _User.Username + "')", con);

            //DataTable da = new DataTable();
            //sda.Fill(da);
            //if (da.Rows.Count > 0)
            //{
            //    return "User already exists...";
            //}
            //else
            //{



            //    SqlCommand cmd = new SqlCommand(" INSERT into dbo.tblUserMaster(Username,Password,Active,JobType,LocationId,MobileNumber,ContactPerson,EmailId,CreatedOn)     VALUES('" + _User.Username + "','" + _User.Password + "','" + _User.Active + "','" + _User.JobType + "','" + _User.LocationId + "','" + _User.MobileNumber + "','" + _User.ContactPerson + "','" + _User.EmailId + "',getdate())", con);
            //    con.Open();
            //    int i = cmd.ExecuteNonQuery();

            //    con.Close();
            //    if (i > 0)
            //    {
            //        return "User Registered Successfully";
            //    }
            //    else
            //    { return "User not registered"; }
            //}

        }
       

        [HttpPost]
        [Route("Login")]
        public Response LoginUser(LoginUser _User)
        {            
        
          return Usermasterextended.LoginValidateForUser(_User,_configuration);

        }
    }
}
