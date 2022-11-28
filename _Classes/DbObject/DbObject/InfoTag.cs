using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace infomanager.DbObject
{    
    public partial class InfoTag : Db
    {
        public int tagId { get; set; }
	public string title { get; set; }

        public InfoTag Create(int tagId, string title)
        {
                string url = $"InfoTag/Create/tagId={tagId}&title={title}";

                return JsonConvert.DeserializeObject<InfoTag>((string)GetData(url).Result);
        }

        public InfoTag Retrieve(int tagId)
        {
                string url = $"InfoTag/tagId={tagId}";

                return JsonConvert.DeserializeObject<InfoTag>((string)GetData(url).Result);
        }
        
        public InfoTag Update(int tagId, string title)
        {
                string url = $"InfoTag/Update/?tagId={tagId}&title={title}";

                return JsonConvert.DeserializeObject<InfoTag>((string)GetData(url).Result);
        }

        public InfoTag Delete(int tagId)
        {
                string url = $"InfoTag/Delete/tagId={tagId}";

                return JsonConvert.DeserializeObject<InfoTag>((string)GetData(url).Result);
        }

        public InfoTag Overview(int maxResult = 500)
        {
                string url = $"InfoTag/Overview/?maxResult={maxResult}";

                return JsonConvert.DeserializeObject<InfoTag>((string)GetData(url).Result);
        }
    }
}