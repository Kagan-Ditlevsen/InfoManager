using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InfoManager.DAL
{
    public static class EnumTables
    {
        public enum ActionType
        {
            PickupGoods = 100,
            DeliverGoods = 110,
            DeliverReturn = 130,
            Standby = 150,
            CustomerPaperwork = 190,
            PickupVehicle = 200,
            PickupTrailer = 210,
            PickupSemiAndTrailer = 220,
            SetdownVehicle = 250,
            SetdownTrailer = 260,
            SetdownSemiAndTrailer = 270,
            WaitTime = 800,
            WaitLoadtime = 802,
            UnexpectedEvent = 810,
            RefuelTruck = 850,
            RefuelTrailer = 852,
            RoadAccident = 886,
            RoadCue = 888,
            DriverBreak = 890,
            CoDriver = 997,
            OtherOwnerWork = 999
        }
        public enum AddressType
        {
            HUB = 1,
            Shop = 2,
            Temporary = 99
        }
        public enum DocumentationType
        {
            NoPaperworkRequired = 100,
            CustomerApp = 110,
            ImagePaper = 250,
            PhsycialPaper = 252
        }
        [Flags]
        public enum UserRole
        {
            Driver = 1,
            RouteManager = 2,
            BetaTester = 64,
            Developer = 128,
            UserManager = 256,
            SystemManager = 512
        }
        public enum UserWorkRole
        {
            Driver = 1,
            CoDriver = 2,
            Trainee = 10,
            TraineeOfficer = 12,
            ADRPersonel = 20,
            ADROfficer = 22,
            SecurityPersonel = 30,
            SecurityOfficer = 32
        }
        public enum VehicleType
        {
            Semi = 110,
            Trailer = 210,
            Truck = 310,
            Lorry = 410,
            Van = 510,
            UnknownVehicle = 999
        }
        public enum WorkStatusType
        {
            WorkdayNoTasks = 101,
            WorkdayUnassigned = 111,
            WorkdayAvailable = 121,
            WorkdayStarted = 131,
            WorkdayFinished = 171,
            WorkdayDocumentationMissing = 199,
            /* */
            Processed = 993,
            Deleted = 995,
            StatusUnknown = 997,
            AttentionRequired = 999
        }
    }
}