using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace infomanager.DbObject
{    
    public partial class InfoEntryType : Db
    {
        public int typeId { get; set; }
	public string internalTitle { get; set; }
	public DateTime createDateTime { get; set; }
	public int? createUserId { get; set; }

        public InfoEntryType Create(int typeId, string internalTitle, DateTime createDateTime, int? createUserId)
        {
                string url = $"InfoEntryType/Create/typeId={typeId}&internalTitle={internalTitle}&createDateTime={createDateTime}&createUserId={createUserId}";

                return JsonConvert.DeserializeObject<InfoEntryType>((string)GetData(url).Result);
        }

        public InfoEntryType Retrieve(int typeId)
        {
                string url = $"InfoEntryType/typeId={typeId}";

                return JsonConvert.DeserializeObject<InfoEntryType>((string)GetData(url).Result);
        }
        
        public InfoEntryType Update(int typeId, string internalTitle)
        {
                string url = $"InfoEntryType/Update/?typeId={typeId}&internalTitle={internalTitle}";

                return JsonConvert.DeserializeObject<InfoEntryType>((string)GetData(url).Result);
        }

        public InfoEntryType Delete(int typeId)
        {
                string url = $"InfoEntryType/Delete/typeId={typeId}";

                return JsonConvert.DeserializeObject<InfoEntryType>((string)GetData(url).Result);
        }

        public InfoEntryType Overview(int maxResult = 500)
        {
                string url = $"InfoEntryType/Overview/?maxResult={maxResult}";

                return JsonConvert.DeserializeObject<InfoEntryType>((string)GetData(url).Result);
        }
    }
}