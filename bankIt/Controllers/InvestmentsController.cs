using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bankIt.Controllers
{
    
    [ApiController]
    [Route("investment")]
    public class InvestmentsController : ControllerBase
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
            SQLDriver.cmd.CommandText = $"SELECT investments FROM bankIt.accounts WHERE username = \"{request.username}\" && password = \"{request.password}\";";
            var result = SQLDriver.cmd.ExecuteReaderAsync().Result;

            result.Read();
            int resultValue = Convert.ToInt32(result[0]);
            result.Close();

            return resultValue;
        }

        [HttpPut]
        public Object Put([FromBody] UpdateCredit request)
        {
            SQLDriver.cmd.CommandText = $"SELECT investments FROM bankIt.accounts WHERE username = \"{request.username}\" && password = \"{request.password}\";";
            var result = SQLDriver.cmd.ExecuteReaderAsync().Result;

            result.Read();
            int val = Convert.ToInt32(result[0]);
            result.Close();


            if (request.credit < 0)
            {
                SQLDriver.cmd.CommandText = $"SELECT investmentFees FROM bankIt.accounts WHERE username = \"{request.username}\" && password = \"{request.password}\";";
                result = SQLDriver.cmd.ExecuteReaderAsync().Result;

                result.Read();
                request.credit -= (Convert.ToDouble(result[0]));
                result.Close();
            }

            Double newBalance = val + request.credit;

            SQLDriver.cmd.CommandText = $"UPDATE bankIt.accounts SET investments = {newBalance} WHERE username = \"{request.username}\" && password = \"{request.password}\";";
            if (SQLDriver.cmd.ExecuteNonQueryAsync().Result == 1)
            {
                return "Operation Successful!";
            }

            return "Operation Failure!";
        }
    }
}
