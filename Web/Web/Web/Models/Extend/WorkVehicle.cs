using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InfoMan.Models
{
    public partial class WorkVehicle
    {
        public static string GetNumberplate(string numberplate, string emptyValue = "-")
        {
            if (string.IsNullOrEmpty(numberplate))
            {
                return emptyValue;
            }

            string tmpPlate = numberplate;
            if (int.TryParse(numberplate, out int id))
            {
                tmpPlate = Common.WorkVehicles.Single(x => x.vehicleId == id).numberplate;
            }
            if (tmpPlate.Length == 6)
            {
                return tmpPlate.Numberplate6();
            }
            return tmpPlate.Numberplate7();
        }
    }
}
