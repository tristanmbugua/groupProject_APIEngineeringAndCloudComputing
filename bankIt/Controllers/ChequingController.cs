using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace bankIt.Controllers
{
    
    [ApiController]
    [Route("chequing")]
    public class ChequingController : ControllerBase
    {
        public class ViewChequing {
            public string username {  get; set; }
            public string password { get; set; }
        }

        public class UpdateChequing
        {
            public string username { get; set; }
            public string password { get; set; }
            public double credit { get; set; }
        }

        [HttpGet]
        public Object Get([FromBody] ViewChequing request)
        {
            try
            {
                SQLDriver.cmd.CommandText = $"SELECT chequing FROM bankIt.accounts WHERE username = \"{request.username}\" && password = \"{request.password}\";";

                using (var result = SQLDriver.cmd.ExecuteReaderAsync().Result)
                {
                    if (result.Read())
                    {
                        int resultValue = Convert.ToInt32(result[0]);
                        result.Close();
                        return $"The current chequing account balance for '{request.username}' is: {resultValue}.";
                    }
                    else
                    {
                        result.Close();
                        return $"Account information for '{request.username}' could not be found or the credentials are incorrect.";
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while attempting to retrieve the chequing account.");
            }
        }

        [HttpPut]
        public Object Put([FromBody] UpdateChequing request)
        {
            try {
                SQLDriver.cmd.CommandText = $"SELECT chequing FROM bankIt.accounts WHERE username = \"{request.username}\" && password = \"{request.password}\";";
                var result = SQLDriver.cmd.ExecuteReaderAsync().Result;

                result.Read();
                int val = Convert.ToInt32(result[0]);
                result.Close();

                Double newBalance = val + request.credit;

                SQLDriver.cmd.CommandText = $"UPDATE bankIt.accounts SET chequing = {newBalance} WHERE username = \"{request.username}\" && password = \"{request.password}\";";
                if (SQLDriver.cmd.ExecuteNonQueryAsync().Result == 1)
                {
                    return $"Chequing account for '{request.username}' successfully updated. New balance: {newBalance}.";
                }
                return "Failed to update the chequing account. Please check the details and try again.";
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An internal error occurred while processing the request. Please try again later.");
            }
        }
    }
}
