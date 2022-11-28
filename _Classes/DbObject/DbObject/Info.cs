using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace infomanager.DbObject
{    
    public partial class Info : Db
    {
        public int infoId { get; set; }
	public int? categoryId { get; set; }
	public string title { get; set; }
	public string remark { get; set; }
	public DateTime createDateTime { get; set; }
	public int createUserId { get; set; }

        public Info Create(int infoId, int? categoryId, string title, string remark, DateTime createDateTime, int createUserId)
        {
                string url = $"Info/Create/infoId={infoId}&categoryId={categoryId}&title={title}&remark={remark}&createDateTime={createDateTime}&createUserId={createUserId}";

                return JsonConvert.DeserializeObject<Info>((string)GetData(url).Result);
        }

        public Info Retrieve(int infoId)
        {
                string url = $"Info/infoId={infoId}";

                return JsonConvert.DeserializeObject<Info>((string)GetData(url).Result);
        }
        
        public Info Update(int infoId, int? categoryId, string title, string remark)
        {
                string url = $"Info/Update/?infoId={infoId}&categoryId={categoryId}&title={title}&remark={remark}";

                return JsonConvert.DeserializeObject<Info>((string)GetData(url).Result);
        }

        public Info Delete(int infoId)
        {
                string url = $"Info/Delete/infoId={infoId}";

                return JsonConvert.DeserializeObject<Info>((string)GetData(url).Result);
        }

        public Info Overview(int maxResult = 500)
        {
                string url = $"Info/Overview/?maxResult={maxResult}";

                return JsonConvert.DeserializeObject<Info>((string)GetData(url).Result);
        }
    }
}