using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace infomanager.DbObject
{    
    public partial class WorkTaskType : Db
    {
        public int typeId { get; set; }
	public string actionType { get; set; }
	public string internalTitle { get; set; }
	public string btnStartText { get; set; }
	public string btnEndText { get; set; }
	public string icon { get; set; }
	public int? requiredFields { get; set; }

        public WorkTaskType Create(int typeId, string actionType, string internalTitle, string btnStartText, string btnEndText, string icon, int? requiredFields)
        {
                string url = $"WorkTaskType/Create/typeId={typeId}&actionType={actionType}&internalTitle={internalTitle}&btnStartText={btnStartText}&btnEndText={btnEndText}&icon={icon}&requiredFields={requiredFields}";

                return JsonConvert.DeserializeObject<WorkTaskType>((string)GetData(url).Result);
        }

        public WorkTaskType Retrieve(int typeId)
        {
                string url = $"WorkTaskType/typeId={typeId}";

                return JsonConvert.DeserializeObject<WorkTaskType>((string)GetData(url).Result);
        }
        
        public WorkTaskType Update(int typeId, string actionType, string internalTitle, string btnStartText, string btnEndText, string icon, int? requiredFields)
        {
                string url = $"WorkTaskType/Update/?typeId={typeId}&actionType={actionType}&internalTitle={internalTitle}&btnStartText={btnStartText}&btnEndText={btnEndText}&icon={icon}&requiredFields={requiredFields}";

                return JsonConvert.DeserializeObject<WorkTaskType>((string)GetData(url).Result);
        }

        public WorkTaskType Delete(int typeId)
        {
                string url = $"WorkTaskType/Delete/typeId={typeId}";

                return JsonConvert.DeserializeObject<WorkTaskType>((string)GetData(url).Result);
        }

        public WorkTaskType Overview(int maxResult = 500)
        {
                string url = $"WorkTaskType/Overview/?maxResult={maxResult}";

                return JsonConvert.DeserializeObject<WorkTaskType>((string)GetData(url).Result);
        }
    }
}