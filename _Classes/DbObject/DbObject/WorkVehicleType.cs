using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace infomanager.DbObject
{    
    public partial class WorkVehicleType : Db
    {
        public int typeId { get; set; }
	public string icon { get; set; }
	public string internalTitle { get; set; }

        public WorkVehicleType Create(int typeId, string icon, string internalTitle)
        {
                string url = $"WorkVehicleType/Create/typeId={typeId}&icon={icon}&internalTitle={internalTitle}";

                return JsonConvert.DeserializeObject<WorkVehicleType>((string)GetData(url).Result);
        }

        public WorkVehicleType Retrieve(int typeId)
        {
                string url = $"WorkVehicleType/typeId={typeId}";

                return JsonConvert.DeserializeObject<WorkVehicleType>((string)GetData(url).Result);
        }
        
        public WorkVehicleType Update(int typeId, string icon, string internalTitle)
        {
                string url = $"WorkVehicleType/Update/?typeId={typeId}&icon={icon}&internalTitle={internalTitle}";

                return JsonConvert.DeserializeObject<WorkVehicleType>((string)GetData(url).Result);
        }

        public WorkVehicleType Delete(int typeId)
        {
                string url = $"WorkVehicleType/Delete/typeId={typeId}";

                return JsonConvert.DeserializeObject<WorkVehicleType>((string)GetData(url).Result);
        }

        public WorkVehicleType Overview(int maxResult = 500)
        {
                string url = $"WorkVehicleType/Overview/?maxResult={maxResult}";

                return JsonConvert.DeserializeObject<WorkVehicleType>((string)GetData(url).Result);
        }
    }
}