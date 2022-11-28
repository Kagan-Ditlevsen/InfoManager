using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace infomanager.DbObject
{    
    public partial class WorkTask : Db
    {
        public Guid taskId { get; set; }
	public Guid workId { get; set; }
	public int typeId { get; set; }
	public int sortOrder { get; set; }
	public DateTime? startDateTime { get; set; }
	public DateTime? endDateTime { get; set; }
	public string addressText { get; set; }
	public string vehicleText { get; set; }
	public string vehicleNumberplate { get; set; }
	public string linkNumberplate { get; set; }
	public string dollyNumberplate { get; set; }
	public string trailerNumberplate { get; set; }
	public string remark { get; set; }
	public string systemRemark { get; set; }
	public DateTime createDateTime { get; set; }
	public int createUserId { get; set; }
	public DateTime? deleteDateTime { get; set; }
	public int? deleteUserId { get; set; }
	public bool? isFinished { get; set; }
	public byte reportColumnNumber { get; set; }
	public string transportId { get; set; }
	public string freighterSetup { get; set; }
	public string addressSetup { get; set; }

        public WorkTask Create(Guid taskId, Guid workId, int typeId, int sortOrder, DateTime? startDateTime, DateTime? endDateTime, string addressText, string vehicleText, string vehicleNumberplate, string linkNumberplate, string dollyNumberplate, string trailerNumberplate, string remark, string systemRemark, DateTime createDateTime, int createUserId, DateTime? deleteDateTime, int? deleteUserId, bool? isFinished, byte reportColumnNumber, string transportId, string freighterSetup, string addressSetup)
        {
                string url = $"WorkTask/Create/taskId={taskId}&workId={workId}&typeId={typeId}&sortOrder={sortOrder}&startDateTime={startDateTime}&endDateTime={endDateTime}&addressText={addressText}&vehicleText={vehicleText}&vehicleNumberplate={vehicleNumberplate}&linkNumberplate={linkNumberplate}&dollyNumberplate={dollyNumberplate}&trailerNumberplate={trailerNumberplate}&remark={remark}&systemRemark={systemRemark}&createDateTime={createDateTime}&createUserId={createUserId}&deleteDateTime={deleteDateTime}&deleteUserId={deleteUserId}&isFinished={isFinished}&reportColumnNumber={reportColumnNumber}&transportId={transportId}&freighterSetup={freighterSetup}&addressSetup={addressSetup}";

                return JsonConvert.DeserializeObject<WorkTask>((string)GetData(url).Result);
        }

        public WorkTask Retrieve(Guid taskId)
        {
                string url = $"WorkTask/taskId={taskId}";

                return JsonConvert.DeserializeObject<WorkTask>((string)GetData(url).Result);
        }
        
        public WorkTask Update(Guid taskId, Guid workId, int typeId, int sortOrder, DateTime? startDateTime, DateTime? endDateTime, string addressText, string vehicleText, string vehicleNumberplate, string linkNumberplate, string dollyNumberplate, string trailerNumberplate, string remark, string systemRemark, DateTime? deleteDateTime, int? deleteUserId, bool? isFinished, byte reportColumnNumber, string transportId, string freighterSetup, string addressSetup)
        {
                string url = $"WorkTask/Update/?taskId={taskId}&workId={workId}&typeId={typeId}&sortOrder={sortOrder}&startDateTime={startDateTime}&endDateTime={endDateTime}&addressText={addressText}&vehicleText={vehicleText}&vehicleNumberplate={vehicleNumberplate}&linkNumberplate={linkNumberplate}&dollyNumberplate={dollyNumberplate}&trailerNumberplate={trailerNumberplate}&remark={remark}&systemRemark={systemRemark}&deleteDateTime={deleteDateTime}&deleteUserId={deleteUserId}&isFinished={isFinished}&reportColumnNumber={reportColumnNumber}&transportId={transportId}&freighterSetup={freighterSetup}&addressSetup={addressSetup}";

                return JsonConvert.DeserializeObject<WorkTask>((string)GetData(url).Result);
        }

        public WorkTask Delete(Guid taskId)
        {
                string url = $"WorkTask/Delete/taskId={taskId}";

                return JsonConvert.DeserializeObject<WorkTask>((string)GetData(url).Result);
        }

        public WorkTask Overview(int maxResult = 500)
        {
                string url = $"WorkTask/Overview/?maxResult={maxResult}";

                return JsonConvert.DeserializeObject<WorkTask>((string)GetData(url).Result);
        }
    }
}