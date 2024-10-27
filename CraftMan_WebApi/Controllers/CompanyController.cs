using CraftMan_WebApi.Models;
using CraftMan_WebApi.ExtendedModels; 
using Microsoft.AspNetCore.Mvc;
namespace CraftMan_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
         [HttpPost]
        [Route("CompanySignUp")]
        public Response Register(CompanyMaster _Company)
        {

            return Companymasterextended.RegistrationCompany(_Company);


        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("CompanySignIn")]
        public Response LoginCompany(LoginComp _Company)
        {

            return Companymasterextended.LoginValidateForCompany(_Company);

        }
    }
}
