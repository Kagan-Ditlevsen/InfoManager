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
            return ApiHelper.DecryptString(id);
        }

        // info.user        WsyKEkzAUf2rLSnwByO¤Zw==        10001
        // haul.user        WsyKEkzAUf2rLSnwByO¤Zw==        B581EBC7-A552-4B8F-88C0-7195AD903E7E
#if DEBUG
        [HttpGet("CreateAuthenticatedUser", Name = "CreateAuthenticatedUser")]
        public string CreateAuthenticatedUser(string id)
        {
            AuthenticatedUser au = new AuthenticatedUser()
            {
                Id = id,
                CreateDateTime = DateTime.Now,
            };
            //return au.CreateDateTime.ToString();

            return JsonConvert.SerializeObject(
                    au,
                    Formatting.None,
                    ApiHelper.serializerSettings
                );
            string encryptedString = ApiHelper.EncryptString(
                JsonConvert.SerializeObject(
                    au,
                    Formatting.None,
                    ApiHelper.serializerSettings
                )
            );

            return encryptedString;
        }

        [HttpGet("GetAuthenticatedUser", Name = "GetAuthenticatedUser")]
        public string GetAuthenticatedUser(string auth)
        {
            dynamic decryptedString = ApiHelper.DecryptString(auth);

            AuthenticatedUser au = new AuthenticatedUser()
            {
                Id = decryptedString.id,
                CreateDateTime = decryptedString.CreateDateTime,
            };

            string rtn = JsonConvert.SerializeObject(au, Formatting.None, ApiHelper.serializerSettings);
            return rtn;
        }
    }
#endif
}