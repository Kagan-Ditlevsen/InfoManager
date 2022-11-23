using api.infomanager.dk.DAL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace api.infomanager.dk.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DailyController : ControllerBase
    {
        [HttpGet("{id}", Name = "DailyRetrieve")]
        public object Retrieve(int id)
        {
            //SqlConnection conn = new SqlConnection();
            //conn.Open();
            //string query = "SELECT * FROM DailyType WHERE typeId = " + id.ToString();
            //SqlCommand cmd = new SqlCommand(query, conn);
            //DataTable dt = new DataTable();
            //dt.Load(cmd.ExecuteReader());
            //conn.Close();

            using(var context = new InfoManager.DAL.infomanager_dk_db_mainEntities())
            {
                var rtn = context.Daily.OrderByDescending(x => x.registerDateTime).Take(1);
                return JsonConvert.SerializeObject(rtn);
            }
        }

        [HttpGet("Overview", Name = "DailyOverview")]
        public object Overview()
        {
            SqlConnection conn = new SqlConnection("data source=mssql.haulmanager.nu;initial catalog=haulmanager_nu_db_im;persist security info=True;user id=haulmanager_nu;password=als76Asl;multipleactiveresultsets=True;application name=infomanager.dk");
            conn.Open();
            string query = "SELECT * FROM DailyType";
            SqlCommand cmd = new SqlCommand(query, conn);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            conn.Close();
            
            return JsonConvert.SerializeObject(dt.DataTableToList<DailyType>());
        }
    }
}