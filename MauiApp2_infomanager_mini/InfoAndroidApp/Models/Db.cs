using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace InfoAndroidApp.Models
{
    internal class Db : IDisposable
    {
        [Obsolete("Not implemented yet")]
        public List<SysUser> SysUser { get; set; }
        [Obsolete("Not implemented yet")]
        public List<Daily> Daily { get; set; }
        [Obsolete("Not implemented yet")]
        public List<DailyInfo> DailyInfo { get; set; }
        public List<DailyType> DailyType { get; set; }
        public List<DailyTypeExtra> DailyTypeExtra { get; set; }
        public List<DailyTypeOption> DailyTypeOption { get; set; }
        /*
         * 
         * <add name="ConnectionStringName"
    providerName="System.Data.SqlClient"
    connectionString="Data Source=mssql.haulmanager.nu;Initial Catalog=haulmanager_nu_db_im;Integrated Security=False;User Id=haulmanager_nu;Password=als76Asl;MultipleActiveResultSets=True" />*/

        public static string SqlConnectionString = "data source=mssql.haulmanager.nu;initial catalog=haulmanager_nu_db_im;persist security info=True;user id=haulmanager_nu;password=als76Asl;MultipleActiveResultSets=False;Encrypt=False;Trusted_Connection=True;TrustServerCertificate=True;Connection Timeout=30;application name=infomanager.dk";

        public Db()
        {
            string setup = Api.ApiCall("setup").Result;
            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(setup);

            SysUser = new List<SysUser>();
            Daily = new List<Daily>();
            DailyInfo = new List<DailyInfo>();

            DailyType = myDeserializedClass.DailyType;
            DailyTypeExtra = myDeserializedClass.DailyTypeExtra;
            DailyTypeOption = myDeserializedClass.DailyTypeOption;

        }
        //public static DataSet DataSetFromReader(IDataReader reader)
        //{
        //    DataSet ds = new DataSet();
        //    while (!reader.IsClosed)
        //    {
        //        DataTable t = new DataTable();
        //        t.Load(reader);
        //        ds.Tables.Add(t);
        //    }
        //    return ds;
        //}
        static SqlConnection Conn()
        {
            SqlConnectionStringBuilder rtn = new();
            rtn.DataSource = "mssql.haulmanager.nu";
            rtn.InitialCatalog = "haulmanager_nu_db_im";
            rtn.UserID = "haulmanager_nu";
            rtn.Password = "als76Asl";
            rtn.PersistSecurityInfo = true;
            rtn.ConnectTimeout = 10;
            rtn.ApplicationName = "IM_App";
            rtn.MultipleActiveResultSets = true;

            rtn.TrustServerCertificate = true;

            return new SqlConnection(rtn.ConnectionString);
        }
        public static async Task<DataSet> DataSetFromReaderAsync(string[] tableNames)
        {
            // "Server=***;Initial Catalog=***;Persist Security Info=False;User  ID=***;Password=***;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;Connection Timeout=30;"

            SqlConnection conn = Conn();
            DataSet result = new DataSet();
            using (DbCommand command = conn.CreateCommand())
            {
                foreach (string table in tableNames)
                {
                    command.CommandText += ";SELECT * FROM " + table;// + " FOR JSON PATH";
                }
                await conn.OpenAsync();

                using (DbDataReader reader = command.ExecuteReader())
                {
                    DataTable table = new DataTable();
                    table.Load(reader);
                    result.Tables.Add(table);
                }
                conn.Close();
            }
            return result;

            //DataSet ds = new DataSet();
            //while (!reader.IsClosed)
            //{
            //    DataTable t = new DataTable();
            //    t.Load(reader);
            //    ds.Tables.Add(t);
            //}
            //return ds;
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
