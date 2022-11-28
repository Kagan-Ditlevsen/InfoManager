using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace infomanager.DbObject
{    
    public partial class InfoInfoTagR : Db
    {
        public int infoId { get; set; }
	public int tagId { get; set; }

        public InfoInfoTagR Create(int infoId, int tagId)
        {
                string url = $"InfoInfoTagR/Create/infoId={infoId}&tagId={tagId}";

                return JsonConvert.DeserializeObject<InfoInfoTagR>((string)GetData(url).Result);
        }

        public InfoInfoTagR Retrieve(int infoId, int tagId)
        {
                string url = $"InfoInfoTagR/infoId={infoId}&tagId={tagId}";

                return JsonConvert.DeserializeObject<InfoInfoTagR>((string)GetData(url).Result);
        }
        
        public InfoInfoTagR Update(int infoId, int tagId)
        {
                string url = $"InfoInfoTagR/Update/?infoId={infoId}&tagId={tagId}";

                return JsonConvert.DeserializeObject<InfoInfoTagR>((string)GetData(url).Result);
        }

        public InfoInfoTagR Delete(int infoId, int tagId)
        {
                string url = $"InfoInfoTagR/Delete/infoId={infoId}&tagId={tagId}";

                return JsonConvert.DeserializeObject<InfoInfoTagR>((string)GetData(url).Result);
        }

        public InfoInfoTagR Overview(int maxResult = 500)
        {
                string url = $"InfoInfoTagR/Overview/?maxResult={maxResult}";

                return JsonConvert.DeserializeObject<InfoInfoTagR>((string)GetData(url).Result);
        }
    }
}