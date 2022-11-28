using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace infomanager.DbObject
{    
    public partial class InfoEntry : Db
    {
        public int entryId { get; set; }
	public int infoId { get; set; }
	public int typeId { get; set; }
	public string title { get; set; }
	public string entry { get; set; }
	public int sortOrder { get; set; }
	public string remark { get; set; }
	public DateTime createDateTime { get; set; }
	public int createUserId { get; set; }

        public InfoEntry Create(int entryId, int infoId, int typeId, string title, string entry, int sortOrder, string remark, DateTime createDateTime, int createUserId)
        {
                string url = $"InfoEntry/Create/entryId={entryId}&infoId={infoId}&typeId={typeId}&title={title}&entry={entry}&sortOrder={sortOrder}&remark={remark}&createDateTime={createDateTime}&createUserId={createUserId}";

                return JsonConvert.DeserializeObject<InfoEntry>((string)GetData(url).Result);
        }

        public InfoEntry Retrieve(int entryId)
        {
                string url = $"InfoEntry/entryId={entryId}";

                return JsonConvert.DeserializeObject<InfoEntry>((string)GetData(url).Result);
        }
        
        public InfoEntry Update(int entryId, int infoId, int typeId, string title, string entry, int sortOrder, string remark)
        {
                string url = $"InfoEntry/Update/?entryId={entryId}&infoId={infoId}&typeId={typeId}&title={title}&entry={entry}&sortOrder={sortOrder}&remark={remark}";

                return JsonConvert.DeserializeObject<InfoEntry>((string)GetData(url).Result);
        }

        public InfoEntry Delete(int entryId)
        {
                string url = $"InfoEntry/Delete/entryId={entryId}";

                return JsonConvert.DeserializeObject<InfoEntry>((string)GetData(url).Result);
        }

        public InfoEntry Overview(int maxResult = 500)
        {
                string url = $"InfoEntry/Overview/?maxResult={maxResult}";

                return JsonConvert.DeserializeObject<InfoEntry>((string)GetData(url).Result);
        }
    }
}