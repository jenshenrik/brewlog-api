using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Brewlog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public TestController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet()]
        public string TestEnvVariables()
        {
            var username = _configuration.GetConnectionString("MongoDbConnectionString");
            if (!String.IsNullOrEmpty(username)) return username;
            return "var not set";
        }
    }
}