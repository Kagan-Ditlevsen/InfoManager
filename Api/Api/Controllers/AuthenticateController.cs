using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace infomanager.Api
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticateController : ControllerBase
    {
        //[HttpGet("Get", Name = "AuthGet")]
        //public string AuthGet(string id)
        //{ 
        //    return EncryptString(id);
        //}

        [HttpGet("{id}", Name = "Auth")]
        public string Auth(string id)
        {
            return ApiHelper.DecryptString(id);
        }
    }
}