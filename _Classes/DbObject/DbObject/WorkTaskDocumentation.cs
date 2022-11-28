using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace infomanager.DbObject
{    
    public partial class WorkTaskDocumentation : Db
    {
        public Guid documentationId { get; set; }
	public Guid workId { get; set; }
	public Guid taskId { get; set; }
	public DateTime registerDateTime { get; set; }
	public string remark { get; set; }
	public DateTime createDateTime { get; set; }
	public int createUserId { get; set; }

        public WorkTaskDocumentation Create(Guid documentationId, Guid workId, Guid taskId, DateTime registerDateTime, string remark, DateTime createDateTime, int createUserId)
        {
                string url = $"WorkTaskDocumentation/Create/documentationId={documentationId}&workId={workId}&taskId={taskId}&registerDateTime={registerDateTime}&remark={remark}&createDateTime={createDateTime}&createUserId={createUserId}";

                return JsonConvert.DeserializeObject<WorkTaskDocumentation>((string)GetData(url).Result);
        }

        public WorkTaskDocumentation Retrieve(Guid documentationId)
        {
                string url = $"WorkTaskDocumentation/documentationId={documentationId}";

                return JsonConvert.DeserializeObject<WorkTaskDocumentation>((string)GetData(url).Result);
        }
        
        public WorkTaskDocumentation Update(Guid documentationId, Guid workId, Guid taskId, DateTime registerDateTime, string remark)
        {
                string url = $"WorkTaskDocumentation/Update/?documentationId={documentationId}&workId={workId}&taskId={taskId}&registerDateTime={registerDateTime}&remark={remark}";

                return JsonConvert.DeserializeObject<WorkTaskDocumentation>((string)GetData(url).Result);
        }

        public WorkTaskDocumentation Delete(Guid documentationId)
        {
                string url = $"WorkTaskDocumentation/Delete/documentationId={documentationId}";

                return JsonConvert.DeserializeObject<WorkTaskDocumentation>((string)GetData(url).Result);
        }

        public WorkTaskDocumentation Overview(int maxResult = 500)
        {
                string url = $"WorkTaskDocumentation/Overview/?maxResult={maxResult}";

                return JsonConvert.DeserializeObject<WorkTaskDocumentation>((string)GetData(url).Result);
        }
    }
}