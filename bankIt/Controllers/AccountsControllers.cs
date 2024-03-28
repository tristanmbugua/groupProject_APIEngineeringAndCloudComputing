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
            SQLDriver.cmd.CommandText = $"INSERT INTO bankIt.accounts(username, password) VALUES(\"{request.username}\", \"{request.password}\");";
            if (SQLDriver.cmd.ExecuteNonQueryAsync().Result == 1)
            {
                return "Operation successful!";
            }

            return "Operation failure!";
        }

        [HttpDelete]
        public Object Delete([FromBody] AccountRequest request)
        {
            SQLDriver.cmd.CommandText = $"DELETE FROM bankIt.accounts WHERE username = \"{request.username}\" && password = \"{request.password}\";";
            
            if (SQLDriver.cmd.ExecuteNonQueryAsync().Result == 1)
            {
                return "Operation successful!";
            }

            return "Operation failure!";
        }

    }
}
