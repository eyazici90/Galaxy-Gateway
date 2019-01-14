using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
 

namespace Galaxy.Gateway.WebSPA.Controllers
{  
    [ApiController]
    public class LogController : ControllerBase
    {
        [Route("api/PGW/Logs")]
        [HttpGet] 
        public IActionResult GetAllLogs() => Ok(new List<object>
        {
            new { Body = "Test Body - 1", Url = "Test Url -1"},
            new { Body = "Test Body - 2", Url = "Test Url -2"}
        });
    }
}
