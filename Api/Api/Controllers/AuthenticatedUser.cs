using System.Security.Authentication;

namespace infomanager.Api
{
    public partial class AuthenticatedUser
    {
        // This should be altered to reflect the real user
        public string Id { get; set; }

        //public static string Retrieve(string id)
        //{
        //    // input id is always string. it could be int or something else in constructor
        //    var user = new AuthenticatedUser() { Id = id };

        //    return user.Id.ToString();
        //}

        public static bool Validate(string auth)
        {
            //throw new AuthenticationException("Authentication key is wrong or out of date");
            return true;
        }
    }
}