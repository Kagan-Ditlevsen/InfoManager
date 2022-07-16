using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InfoMan.Models
{
    public partial class WorkAddress
    {
        public string Address()
        {
            string rtn = gpsAddress;
            if (!string.IsNullOrEmpty(title))
            {
                rtn = title + ", " + rtn;
            }
            return rtn.Trim();
        }
    }
}
