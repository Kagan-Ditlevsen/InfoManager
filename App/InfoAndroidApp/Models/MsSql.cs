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
    internal class MsSql : IDisposable
    {
        public static string SqlConnectionString = "data source=mssql.haulmanager.nu;initial catalog=haulmanager_nu_db_im;persist security info=True;user id=haulmanager_nu;password=als76Asl;MultipleActiveResultSets=False;Encrypt=False;Trusted_Connection=True;TrustServerCertificate=True;Connection Timeout=30;application name=infomanager.dk";

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
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
