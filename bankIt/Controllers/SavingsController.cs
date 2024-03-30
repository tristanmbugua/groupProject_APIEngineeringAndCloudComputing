using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace bankIt.Controllers
{
    
    [ApiController]
    [Route("saving")]
    public class SavingsController : ControllerBase
    {
        public class ViewSavings
        {
            public string username { get; set; }
            public string password { get; set; }
        }

        public class UpdateSavings
        {
            public string username { get; set; }
            public string password { get; set; }
            public double credit { get; set; }
        }

        [HttpGet]
        public Object Get([FromBody] ViewSavings request)
        {
            try
            {
                SQLDriver.cmd.CommandText = $"SELECT savings FROM bankIt.accounts WHERE username = \"{request.username}\" && password = \"{request.password}\";";
                using (var result = SQLDriver.cmd.ExecuteReaderAsync().Result)
                {
                    if (result.Read())
                    {
                        int resultValue = Convert.ToInt32(result[0]);
                        result.Close();
                        return $"The current saving balance for '{request.username}' is: {resultValue}.";
                    }
                    else
                    {
                        result.Close();
                        return $"No saving information found for username: '{request.username}'. Please verify the account details.";
                    }
                };
            }catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while attempting to retrieve the saving account.");
            }

        }

        [HttpPut]
        public Object Put([FromBody] UpdateSavings request)
        {
            try {
                SQLDriver.cmd.CommandText = $"SELECT savings FROM bankIt.accounts WHERE username = \"{request.username}\" && password = \"{request.password}\";";
                var result = SQLDriver.cmd.ExecuteReaderAsync().Result;

                result.Read();
                int val = Convert.ToInt32(result[0]);
                result.Close();

                if (request.credit < 0)
                {
                    SQLDriver.cmd.CommandText = $"SELECT savingFees FROM bankIt.accounts WHERE username = \"{request.username}\" && password = \"{request.password}\";";
                    result = SQLDriver.cmd.ExecuteReaderAsync().Result;

                    result.Read();
                    request.credit -= (Convert.ToDouble(result[0]));
                    result.Close();
                }

                Double newBalance = val + request.credit;

                SQLDriver.cmd.CommandText = $"UPDATE bankIt.accounts SET savings = {newBalance} WHERE username = \"{request.username}\" && password = \"{request.password}\";";
                if (SQLDriver.cmd.ExecuteNonQueryAsync().Result > 0)
                {
                    return $"Savings updated successfully. New balance: {newBalance}.";
                }

                return "Failed to update savings. Please check the account details and try again.";
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating savings.");
            }
        }
    }
}
