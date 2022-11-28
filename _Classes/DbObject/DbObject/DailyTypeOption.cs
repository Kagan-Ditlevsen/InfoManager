using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace infomanager.DbObject
{    
    public partial class DailyTypeOption : Db
    {
        public int optionId { get; set; }
	public int typeId { get; set; }
	public string internalTitle { get; set; }
	public int sortOrder { get; set; }
	public bool isActive { get; set; }
	public string iconCss { get; set; }
	public string description { get; set; }

        public DailyTypeOption Create(int optionId, int typeId, string internalTitle, int sortOrder, bool isActive, string iconCss, string description)
        {
                string url = $"DailyTypeOption/Create/optionId={optionId}&typeId={typeId}&internalTitle={internalTitle}&sortOrder={sortOrder}&isActive={isActive}&iconCss={iconCss}&description={description}";

                return JsonConvert.DeserializeObject<DailyTypeOption>((string)GetData(url).Result);
        }

        public DailyTypeOption Retrieve(int optionId)
        {
                string url = $"DailyTypeOption/optionId={optionId}";

                return JsonConvert.DeserializeObject<DailyTypeOption>((string)GetData(url).Result);
        }
        
        public DailyTypeOption Update(int optionId, int typeId, string internalTitle, int sortOrder, bool isActive, string iconCss, string description)
        {
                string url = $"DailyTypeOption/Update/?optionId={optionId}&typeId={typeId}&internalTitle={internalTitle}&sortOrder={sortOrder}&isActive={isActive}&iconCss={iconCss}&description={description}";

                return JsonConvert.DeserializeObject<DailyTypeOption>((string)GetData(url).Result);
        }

        public DailyTypeOption Delete(int optionId)
        {
                string url = $"DailyTypeOption/Delete/optionId={optionId}";

                return JsonConvert.DeserializeObject<DailyTypeOption>((string)GetData(url).Result);
        }

        public DailyTypeOption Overview(int maxResult = 500)
        {
                string url = $"DailyTypeOption/Overview/?maxResult={maxResult}";

                return JsonConvert.DeserializeObject<DailyTypeOption>((string)GetData(url).Result);
        }
    }
}