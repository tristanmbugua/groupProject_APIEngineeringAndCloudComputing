using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bankIt.Controllers
{
    
    [ApiController]
    [Route("transfer")]
    public class TransfersController : ControllerBase
    {
        public class TransferRequestInternal
        {
            public string username { get; set; }
            public string password { get; set; }
            public string toAccount { get; set; }
            public string fromAccount { get; set; }
            public double deposit { get; set; }

        }

        public class TransferRequestExternal
        {
            public string username { get; set; }
            public string password { get; set; }
            public string toAccount { get; set; }
            public double deposit { get; set; }
        }

        [HttpPut("external")]
        public Object Get([FromBody] TransferRequestExternal request)
        {
            try
            {
                if (request.deposit > 0)
                {
                    /*SQLDriver.cmd.CommandText = $"UPDATE bankIt.accounts SET savings = {newBalance} WHERE username = \"{request.username}\" && password = \"{request.password}\";";*/
                    SQLDriver.cmd.CommandText = $"UPDATE bankIt.accounts SET chequing = chequing - {request.deposit} WHERE username = \"{request.username}\" && password = \"{request.password}\";" +
                        $" UPDATE bankIt.accounts SET chequing = chequing + {request.deposit} WHERE username = \"{request.toAccount}\";";
                    if (SQLDriver.cmd.ExecuteNonQueryAsync().Result > 0)
                    {
                        return $"Transfer of {request.deposit} has been successfully withdrawn from {request.username} and deposited into {request.toAccount}.";
                    }

                    return "Failed to transfer. Please check the account details and try again.";
                }

                return "Error. Tried to deposit negative amount of money.";
            } catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while transferring. Please contact our IT team.");
            }
            
        }

        [HttpPut("internal")]
        public Object Put([FromBody] TransferRequestInternal request)
        {
            try
            {
                if (request.deposit > 0)
                {
                    /*SQLDriver.cmd.CommandText = $"UPDATE bankIt.accounts SET savings = {newBalance} WHERE username = \"{request.username}\" && password = \"{request.password}\";";*/
                    SQLDriver.cmd.CommandText =
                        $"UPDATE bankIt.accounts SET {request.fromAccount} = {request.fromAccount} - {request.deposit} WHERE username = \"{request.username}\" && password = \"{request.password}\";" +
                        $" UPDATE bankIt.accounts SET {request.toAccount} = {request.toAccount} + {request.deposit} WHERE username = \"{request.username}\" && password = \"{request.password}\";";
                    if (SQLDriver.cmd.ExecuteNonQueryAsync().Result > 0)
                    {
                        return $"Transfer of {request.deposit} has been successfully withdrawn from internal account {request.fromAccount} and deposited into internal account {request.toAccount} for user {request.username}.";
                    }

                    return "Failed to transfer. Please check the account details and try again.";
                }

                return "Error. Tried to deposit negative amount of money.";
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while transferring. Please contact our IT team.");
            }
        }
    }
}
