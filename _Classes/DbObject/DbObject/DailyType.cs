using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace infomanager.DbObject
{    
    public partial class DailyType : Db
    {
        public int typeId { get; set; }
	public int? parentTypeId { get; set; }
	public int? nextTypeId { get; set; }
	public string internalTitle { get; set; }
	public bool isActive { get; set; }
	public bool isFavorite { get; set; }
	public int sortOrder { get; set; }
	public int? defaultOptionId { get; set; }
	public string iconCss { get; set; }
	public string description { get; set; }

        public DailyType Create(int typeId, int? parentTypeId, int? nextTypeId, string internalTitle, bool isActive, bool isFavorite, int sortOrder, int? defaultOptionId, string iconCss, string description)
        {
                string url = $"DailyType/Create/typeId={typeId}&parentTypeId={parentTypeId}&nextTypeId={nextTypeId}&internalTitle={internalTitle}&isActive={isActive}&isFavorite={isFavorite}&sortOrder={sortOrder}&defaultOptionId={defaultOptionId}&iconCss={iconCss}&description={description}";

                return JsonConvert.DeserializeObject<DailyType>((string)GetData(url).Result);
        }

        public DailyType Retrieve(int typeId)
        {
                string url = $"DailyType/typeId={typeId}";

                return JsonConvert.DeserializeObject<DailyType>((string)GetData(url).Result);
        }
        
        public DailyType Update(int typeId, int? parentTypeId, int? nextTypeId, string internalTitle, bool isActive, bool isFavorite, int sortOrder, int? defaultOptionId, string iconCss, string description)
        {
                string url = $"DailyType/Update/?typeId={typeId}&parentTypeId={parentTypeId}&nextTypeId={nextTypeId}&internalTitle={internalTitle}&isActive={isActive}&isFavorite={isFavorite}&sortOrder={sortOrder}&defaultOptionId={defaultOptionId}&iconCss={iconCss}&description={description}";

                return JsonConvert.DeserializeObject<DailyType>((string)GetData(url).Result);
        }

        public DailyType Delete(int typeId)
        {
                string url = $"DailyType/Delete/typeId={typeId}";

                return JsonConvert.DeserializeObject<DailyType>((string)GetData(url).Result);
        }

        public DailyType Overview(int maxResult = 500)
        {
                string url = $"DailyType/Overview/?maxResult={maxResult}";

                return JsonConvert.DeserializeObject<DailyType>((string)GetData(url).Result);
        }
    }
}