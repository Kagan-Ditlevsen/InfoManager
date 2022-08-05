using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dk.infomanager.Models
{
    public partial class WorkTaskType
    {
        public static WorkTaskType FromEnum(WorkTaskTypeEnum givenEnum)
        {
            return Common.WorkTaskTypes.Single(x => x.typeId == (int)givenEnum);
        }
        public string IconNoFormat()
        {
            return icon
                .Replace("text-success", "")
                .Replace("text-danger", "")
                .Replace("bg-success", "")
                .Replace("bg-danger", "")
                .Replace("  ", "");
        }
    }
}
