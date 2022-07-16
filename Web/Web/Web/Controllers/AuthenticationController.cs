using InfoMan.Models;
using System;
using System.Web;
using System.Web.Mvc;
using Skd.Web;

namespace InfoMan.Controllers
{
    public class AuthenticationController : Controller
    {
        [HttpGet]
        public ActionResult Logon() => View();
        [HttpPost]
        public void Logon(string logonId, string password, string rUrl = "")
        {
            try
            {
                using (var db = new Db())
                {
                    var user = SysUser.Retrieve(logonId, password);
                    if (user != null)
                    {
                        HttpCookie mycookie = new HttpCookie(Settings.AppSetting("Skd:Cookie:User"))
                        {
                            SameSite = SameSiteMode.Lax,
                            Secure = true,
                            Expires = DateTime.Now.AddDays(90),
                            Value = Skd.Password.EncryptDES(
                                Skd.Web.Settings.AppSetting("Skd:PublicKey"),
                                user.userId.ToString())
                        };
                        Response.Cookies.Add(mycookie);

                        new Message()
                        {
                            MessageType = Message.MessageTypeEnum.Success,
                            Title = "You were logged on with success",
                            SubTitle = "Logged on using cookie"
                        };

                        Response.Redirect(rUrl, false);
                    }
                    else
                    {
                        TempData["errorMsg"] = "The credentials provided is not correct";
                        Response.Redirect(rUrl, true);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong.\n" + e.Message);
            }
        }

        public void Logoff()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Abandon();

            if (HttpContext.Request.Cookies["infoMan"] != null)
            {
                HttpCookie cookie = new HttpCookie("infoMan");
                cookie.Expires = DateTime.MinValue;
                cookie.Value = "";
                HttpContext.Response.Cookies.Add(cookie);
            }

            string rUrl = Request.UrlReferrer.ToString();
            if(string.IsNullOrEmpty(rUrl))
            {
                rUrl = "/";
            }
            HttpContext.Response.Redirect(rUrl);
        }
    }
}