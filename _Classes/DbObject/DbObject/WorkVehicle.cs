using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace infomanager.DbObject
{    
    public partial class WorkVehicle : Db
    {
        public int vehicleId { get; set; }
	public string numberplate { get; set; }
	public int typeId { get; set; }
	public bool isArchived { get; set; }
	public bool isTemporary { get; set; }
	public string lastLocation { get; set; }
	public string remark { get; set; }
	public DateTime createDateTime { get; set; }
	public int createUserId { get; set; }
	public string internalId { get; set; }

        public WorkVehicle Create(int vehicleId, string numberplate, int typeId, bool isArchived, bool isTemporary, string lastLocation, string remark, DateTime createDateTime, int createUserId, string internalId)
        {
                string url = $"WorkVehicle/Create/vehicleId={vehicleId}&numberplate={numberplate}&typeId={typeId}&isArchived={isArchived}&isTemporary={isTemporary}&lastLocation={lastLocation}&remark={remark}&createDateTime={createDateTime}&createUserId={createUserId}&internalId={internalId}";

                return JsonConvert.DeserializeObject<WorkVehicle>((string)GetData(url).Result);
        }

        public WorkVehicle Retrieve(int vehicleId)
        {
                string url = $"WorkVehicle/vehicleId={vehicleId}";

                return JsonConvert.DeserializeObject<WorkVehicle>((string)GetData(url).Result);
        }
        
        public WorkVehicle Update(int vehicleId, string numberplate, int typeId, bool isArchived, bool isTemporary, string lastLocation, string remark, string internalId)
        {
                string url = $"WorkVehicle/Update/?vehicleId={vehicleId}&numberplate={numberplate}&typeId={typeId}&isArchived={isArchived}&isTemporary={isTemporary}&lastLocation={lastLocation}&remark={remark}&internalId={internalId}";

                return JsonConvert.DeserializeObject<WorkVehicle>((string)GetData(url).Result);
        }

        public WorkVehicle Delete(int vehicleId)
        {
                string url = $"WorkVehicle/Delete/vehicleId={vehicleId}";

                return JsonConvert.DeserializeObject<WorkVehicle>((string)GetData(url).Result);
        }

        public WorkVehicle Overview(int maxResult = 500)
        {
                string url = $"WorkVehicle/Overview/?maxResult={maxResult}";

                return JsonConvert.DeserializeObject<WorkVehicle>((string)GetData(url).Result);
        }
    }
}