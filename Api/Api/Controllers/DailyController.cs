using api.infomanager.dk.DAL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml.Serialization;
using System.IO;
using System.Dynamic;
using InfoManager.DAL;

namespace api.infomanager.dk.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DailyController : ControllerBase
    {
        [HttpGet("{id}", Name = "DailyRetrieve")]
        public object Retrieve(Guid id)
        {
            //return DbHelper.Retrieve(id);
            using (var context = new infomanager_dk_db_mainEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                var rtn = context.Daily.Find(id);
                return JsonConvert.SerializeObject(rtn, Formatting.None, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    MaxDepth = 1
                });
            }
        }

        [HttpGet("Overview", Name = "DailyOverview")]
        public object Overview()
        {
            using (var context = new infomanager_dk_db_mainEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                var rtn = context.Daily.Take(5).ToList();
                return JsonConvert.SerializeObject(rtn, Formatting.None, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    MaxDepth = 1
                });
            }
        }
        /*
         Foo foo = new Foo {A = 1, B = "abc"};
        foreach(var prop in foo.GetType().GetProperties()) {
            Console.WriteLine("{0}={1}", prop.Name, prop.GetValue(foo, null));
        }
         * */
        /*
        dynamic expando = new ExpandoObject();
        var p = expando as IDictionary<String, object>;

        p["A"] = "New val 1";
        p["B"] = "New val 2";

        Console.WriteLine(expando.A);
        Console.WriteLine(expando.B);
         * 
         * */
    }
}