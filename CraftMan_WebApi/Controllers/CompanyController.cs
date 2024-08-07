using CraftMan_WebApi.Models;
using CraftMan_WebApi.ExtendedModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Data;

namespace CraftMan_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        [HttpPost]
        [Route("RegisterCompany")]
        public Response RegisterCompany(CompanyMaster _Company)
        {

            return Companymasterextended.RegistrationCompany(_Company, _configuration);


        }
        [HttpPost]
        [Route("LoginCompany")]
        public Response LoginCompany(LoginUser _User)
        {

            return Companymasterextended.LoginValidateForCompany(_User, _configuration);

        }
    }
}
