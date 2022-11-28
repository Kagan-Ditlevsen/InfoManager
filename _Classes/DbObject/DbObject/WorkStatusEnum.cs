using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace infomanager.DbObject
{    
    public partial class WorkStatusEnum : Db
    {
        public int statusId { get; set; }
	public string internalTitle { get; set; }

        public WorkStatusEnum Create(int statusId, string internalTitle)
        {
                string url = $"WorkStatusEnum/Create/statusId={statusId}&internalTitle={internalTitle}";

                return JsonConvert.DeserializeObject<WorkStatusEnum>((string)GetData(url).Result);
        }

        public WorkStatusEnum Retrieve(int statusId)
        {
                string url = $"WorkStatusEnum/statusId={statusId}";

                return JsonConvert.DeserializeObject<WorkStatusEnum>((string)GetData(url).Result);
        }
        
        public WorkStatusEnum Update(int statusId, string internalTitle)
        {
                string url = $"WorkStatusEnum/Update/?statusId={statusId}&internalTitle={internalTitle}";

                return JsonConvert.DeserializeObject<WorkStatusEnum>((string)GetData(url).Result);
        }

        public WorkStatusEnum Delete(int statusId)
        {
                string url = $"WorkStatusEnum/Delete/statusId={statusId}";

                return JsonConvert.DeserializeObject<WorkStatusEnum>((string)GetData(url).Result);
        }

        public WorkStatusEnum Overview(int maxResult = 500)
        {
                string url = $"WorkStatusEnum/Overview/?maxResult={maxResult}";

                return JsonConvert.DeserializeObject<WorkStatusEnum>((string)GetData(url).Result);
        }
    }
}