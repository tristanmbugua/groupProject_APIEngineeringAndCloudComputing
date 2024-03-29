using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bankIt.Controllers
{
    
    [ApiController]
    [Route("creditcard")]
    public class CreditCardController : ControllerBase
    {
        public class ViewCredit
        {
            public string username { get; set; }
            public string password { get; set; }
        }

        public class UpdateCredit
        {
            public string username { get; set; }
            public string password { get; set; }
            public double credit { get; set; }
        }

        [HttpGet]
        public Object Get([FromBody] ViewCredit request)
        {
            try
            {
                SQLDriver.cmd.CommandText = $"SELECT creditCard FROM bankIt.accounts WHERE username = \"{request.username}\" && password = \"{request.password}\";";

                using (var result = SQLDriver.cmd.ExecuteReaderAsync().Result)
                {
                    if (result.Read())
                    {
                        int resultValue = Convert.ToInt32(result[0]);
                        result.Close();
                        return $"Credit card account successfully retrieved: {resultValue}.";
                    }
                    else
                    {
                        result.Close();
                        return "Account not found or incorrect credentials.";
                    }
                } ;
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An internal error occurred while processing the request. Please try again later.");
            }
        }

        [HttpPut]
        public Object Put([FromBody] UpdateCredit request)
        {
            try {
                SQLDriver.cmd.CommandText = $"SELECT creditCard FROM bankIt.accounts WHERE username = \"{request.username}\" && password = \"{request.password}\";";
                var result = SQLDriver.cmd.ExecuteReaderAsync().Result;

                result.Read();
                int val = Convert.ToInt32(result[0]);
                result.Close();


                if (request.credit < 0)
                {
                    SQLDriver.cmd.CommandText = $"SELECT creditcardFees FROM bankIt.accounts WHERE username = \"{request.username}\" && password = \"{request.password}\";";
                    result = SQLDriver.cmd.ExecuteReaderAsync().Result;

                    result.Read();
                    request.credit -= ((Convert.ToDouble(result[0])));
                    result.Close();
                }

                Double newBalance = val + request.credit;

                SQLDriver.cmd.CommandText = $"UPDATE bankIt.accounts SET creditCard = {newBalance} WHERE username = \"{request.username}\" && password = \"{request.password}\";";
                if (SQLDriver.cmd.ExecuteNonQueryAsync().Result == 1)
                {
                    return $"Credit card balance successfully updated for user '{request.username}'. New balance: {newBalance}.";
                }

                return $"Failed to update the credit card balance for '{request.username}'. Ensure the account details are correct.";
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An internal error occurred while processing the request. Please try again later.");
            }
        }
    }
}
