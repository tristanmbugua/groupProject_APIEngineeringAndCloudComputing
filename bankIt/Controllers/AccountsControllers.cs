using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bankIt.Controllers
{
    
    [ApiController]
    [Route("account")]
    public class AccountsController : ControllerBase
    {
        public class AccountRequest
        {
            public string username { get; set; }
            public string password { get; set; }
        }

        [HttpPost]
        public Object Get([FromBody] AccountRequest request)
        {
            try {
                SQLDriver.cmd.CommandText = $"INSERT INTO bankIt.accounts(username, password) VALUES(\"{request.username}\", \"{request.password}\");";
                if (SQLDriver.cmd.ExecuteNonQueryAsync().Result == 1)
                {
                    return "The account for user '" + request.username + "' was successfully created."; 
                }

                return $"Failed to create the account for '{request.username}'. Please ensure the data is correct and try again.";
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the account. Please contact support if this continues.");
            }
        }

        [HttpDelete]
        public Object Delete([FromBody] AccountRequest request)
        {
            try {
                SQLDriver.cmd.CommandText = $"DELETE FROM bankIt.accounts WHERE username = \"{request.username}\" && password = \"{request.password}\";";

                if (SQLDriver.cmd.ExecuteNonQueryAsync().Result == 1)
                {
                    return $"The account for user '{request.username}' has been successfully deleted.";
                }

                return "Account deletion failed. Either no matching account was found, or the provided credentials were incorrect.";
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the account. Please contact support if this continues.");
            }
        }       
    }
}
