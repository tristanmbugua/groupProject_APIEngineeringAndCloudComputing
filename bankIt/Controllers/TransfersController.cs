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
            public string toUser { get; set; }
            public string fromUser { get; set; }
            public double deposit { get; set; }

        }

        public class TransferRequestExternal
        {
            public string toAccount { get; set; }
            public string fromAccount { get; set; }
            public double deposit { get; set; }
        }

        [HttpPut("internal")]
        public Object Get([FromBody] TransferRequestInternal request)
        {
            /*SQLDriver.cmd.CommandText = $"";
            return SQLDriver.cmd.___().Result;*/

            return "Finish this.";
        }

        [HttpPut("external")]
        public Object Put([FromBody] TransferRequestExternal request)
        {
            /*SQLDriver.cmd.CommandText = $"";
            return SQLDriver.cmd.___().Result;*/

            return "Finish this.";
        }
    }
}
