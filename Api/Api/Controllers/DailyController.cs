using api.infomanager.dk.DAL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml.Serialization;
using System.IO;

namespace api.infomanager.dk.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DailyController : ControllerBase
    {
        [HttpGet("{id}", Name = "DailyRetrieve")]
        public object Retrieve(int id)
        {
            using (var context = new InfoManager.DAL.infomanager_dk_db_mainEntities())
            {
                var rtn = context.SysUser.SingleOrDefault(x => x.userId == id);
                return JsonConvert.SerializeObject(rtn, Formatting.None, new JsonSerializerSettings()
                {
                    MaxDepth = 1,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                });
            }
        }

        [HttpGet("Overview", Name = "DailyOverview")]
        public object Overview()
        {
            using (var context = new InfoManager.DAL.infomanager_dk_db_mainEntities())
            {
                var rtn = context.Daily;
                var q = from daily in context.Daily select new { id = daily.dailyId };
                return JsonConvert.SerializeObject(q);
            }
        }
    }
}