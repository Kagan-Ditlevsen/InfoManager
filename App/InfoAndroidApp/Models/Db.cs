using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoAndroidApp.Models
{
    // https://json2csharp.com/
    internal partial class Db : IDisposable // From ROOT
    {
        // Db myDeserializedClass = JsonConvert.DeserializeObject<Db>(myJsonResponse);

        public SysUser SysUser { get; set; }
        public Daily Daily { get; set; }
        public DailyInfo DailyInfo { get; set; }
        public List<DailyType> DailyType { get; set; }
        public List<DailyTypeExtra> DailyTypeExtra { get; set; }
        public List<DailyTypeOption> DailyTypeOption { get; set; }

        public Db Conn(bool refreshDb=false)
        {
            if (refreshDb)
            {
                _apiResult = "";
            }
            return JsonConvert.DeserializeObject<Db>(Api.Get("setup").Result);
        }
        private string _apiResult { get; set; }
        private string ApiResult
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_apiResult))
                {
                    _apiResult = Api.Get("setup").Result;
                }
                return _apiResult;
            }
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
