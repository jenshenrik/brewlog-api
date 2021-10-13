using System;
using Microsoft.AspNetCore.Mvc;

namespace Brewlog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet()]
        public string TestEnvVariables()
        {
            var username = Environment.GetEnvironmentVariable("mongodbusername");
            if (!String.IsNullOrEmpty(username)) return username;
            return "var not set";
        }
    }
}