using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace infomanager.DbObject
{    
    public partial class DailyTypeExtra : Db
    {
        public int extraId { get; set; }
	public int typeId { get; set; }
	public string internalTitle { get; set; }
	public int sortOrder { get; set; }
	public bool isActive { get; set; }
	public bool isRequired { get; set; }
	public string inputType { get; set; }
	public string inputData { get; set; }

        public DailyTypeExtra Create(int extraId, int typeId, string internalTitle, int sortOrder, bool isActive, bool isRequired, string inputType, string inputData)
        {
                string url = $"DailyTypeExtra/Create/extraId={extraId}&typeId={typeId}&internalTitle={internalTitle}&sortOrder={sortOrder}&isActive={isActive}&isRequired={isRequired}&inputType={inputType}&inputData={inputData}";

                return JsonConvert.DeserializeObject<DailyTypeExtra>((string)GetData(url).Result);
        }

        public DailyTypeExtra Retrieve(int extraId)
        {
                string url = $"DailyTypeExtra/extraId={extraId}";

                return JsonConvert.DeserializeObject<DailyTypeExtra>((string)GetData(url).Result);
        }
        
        public DailyTypeExtra Update(int extraId, int typeId, string internalTitle, int sortOrder, bool isActive, bool isRequired, string inputType, string inputData)
        {
                string url = $"DailyTypeExtra/Update/?extraId={extraId}&typeId={typeId}&internalTitle={internalTitle}&sortOrder={sortOrder}&isActive={isActive}&isRequired={isRequired}&inputType={inputType}&inputData={inputData}";

                return JsonConvert.DeserializeObject<DailyTypeExtra>((string)GetData(url).Result);
        }

        public DailyTypeExtra Delete(int extraId)
        {
                string url = $"DailyTypeExtra/Delete/extraId={extraId}";

                return JsonConvert.DeserializeObject<DailyTypeExtra>((string)GetData(url).Result);
        }

        public DailyTypeExtra Overview(int maxResult = 500)
        {
                string url = $"DailyTypeExtra/Overview/?maxResult={maxResult}";

                return JsonConvert.DeserializeObject<DailyTypeExtra>((string)GetData(url).Result);
        }
    }
}