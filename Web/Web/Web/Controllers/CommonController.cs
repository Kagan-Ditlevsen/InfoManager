using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Skd.Web;
using System.Threading;
using System.Globalization;

namespace dk.infomanager.Controllers
{
    [OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
    public class CommonController : Controller
    {
        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            if (!requestContext.HttpContext.Request.IsSecureConnection)
            {
                var uriBuilder = new UriBuilder(requestContext.HttpContext.Request.Url)
                {
                    Scheme = Uri.UriSchemeHttps,
                    Port = 443
                };
                //throw new Exception(uriBuilder.Uri.ToString());
                Server.TransferRequest(uriBuilder.Uri.ToString(), true);

                //throw new UnauthorizedAccessException("Secure connection is required. Try " + requestContext.HttpContext.Request.Url.ToString().Replace("http://", "https://") + " instead.");
            }


            CultureInfo ci = new CultureInfo("en-US");
            ci.DateTimeFormat.LongTimePattern = "HH:mm:ss";
            ci.DateTimeFormat.ShortTimePattern = "HH:mm:ss";

            ci.DateTimeFormat.LongDatePattern = "yyyy-MM-dd";
            ci.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            ci.DateTimeFormat.YearMonthPattern = "yyyy-MM";
            ci.DateTimeFormat.MonthDayPattern = "MM-dd";
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            var req = requestContext.HttpContext;
            var session = req.Session;
            var cookies = req.Request.Cookies;

            if (session == null || (session != null && session["User"] == null))
            {
                if (cookies[Settings.AppSetting("Skd:Cookie:User")] != null && !string.IsNullOrWhiteSpace(cookies[Settings.AppSetting("Skd:Cookie:User")].Value))
                {
                    string decryptedValue = Skd.Password.DecryptDES(
                        Settings.AppSetting("Skd:PublicKey"),
                        cookies.Get(Settings.AppSetting("Skd:Cookie:User")).Value);

                    if (int.TryParse(decryptedValue, out int userId))
                    {
                        var user = Models.SysUser.Retrieve(userId);
                        session["User.LogonDateTime"] = DateTime.Now;

                        new Message()
                        {
                            MessageType = Message.MessageTypeEnum.Success,
                            Title = "Logon Success",
                            SubTitle = "You were logged on using cookie"
                        };
                    }
                    else
                    {
                        throw new Exception("The stored AutoLogon cookie is corrupt or the stored value is incorrect. Please contact support to resolve this issue.");
                    }
                }
                else
                {
                    //session["logonRUrl"] = req.Request.Url.ToString();
                    requestContext.HttpContext.Response.Redirect("/Authentication/Logon?rUrl=" + req.Request.Url);
                    //req.Server.TransferRequest("/Authentication/Logon?rUrl=" + req.Request.Url);
                }
            }

            return base.BeginExecute(requestContext, callback, state);
        }
    }
}