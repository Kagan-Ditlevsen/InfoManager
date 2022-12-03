using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace infomanager.Api
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticateController : ControllerBase
    {
        [HttpGet("{id}", Name = "Auth")]
        public string Auth(string id)
        {
            /*
                - decrypt "id" into
                    - input format { #database setup user data#, authenticationDateTime: *datetime of the creation of the input* }
                - authenticate user according to the database setup
                - user
                    - is valid
                        enctypt: { result: "ok", authenticationDateTime: *datetime of the creation of the output* }
                    - is invalid
                        enctypt: { result: "error", authenticationDateTime: *datetime of the creation of the output*, error: *error.message* }
                - send result ( => return result )
            */
            return ApiHelper.DecryptString(id);
        }
    }
}