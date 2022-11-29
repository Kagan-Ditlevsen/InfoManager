using Newtonsoft.Json;
using System.IO;
using System.Security.Authentication;
using System.Text;

namespace infomanager.Api
{
    public partial class AuthenticatedUser
    {
        // This should be altered to reflect the real user
        public string Id { get; set; }
        public DateTime CreateDateTime { get; set; }

        //public static string Retrieve(string id)
        //{
        //    // input id is always string. it could be int or something else in constructor
        //    var user = new AuthenticatedUser() { Id = id };

        //    return user.Id.ToString();
        //}

        public static bool Validate(string auth)
        {
            dynamic decryptedString = ApiHelper.DecryptString(auth);

            AuthenticatedUser au = new AuthenticatedUser()
            {
                Id = decryptedString.id,
                CreateDateTime = decryptedString.CreateDateTime,
            };

            //throw new AuthenticationException("Authentication key is wrong or out of date");
            return true;
        }
    }
}