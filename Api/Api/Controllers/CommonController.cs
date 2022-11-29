using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Text;
using infomanager.DAL;

namespace infomanager.Api
{
    [ApiController]
    [Route("[controller]")]
    public partial class CommonController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //throw new Exception("place security here: infomanager.Api.CommonController");
            base.OnActionExecuting(context);
        }
    }
}