using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace infomanager.DbObject
{
    public partial class Db
    {
        private const string BASE_URL = "https://im.api.infomanager.dk";

        public static async Task<object> GetData(string url)
        {
            string rtn = "";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    rtn = await client.GetStringAsync(BASE_URL + url);
                }
                catch (HttpRequestException e)
                {
                    rtn = "\nException Caught!";
                    rtn += $"Message :{e.Message}";
                }
            }
            return rtn;
        }
    }
}