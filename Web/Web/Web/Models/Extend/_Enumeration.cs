using System;

namespace InfoMan.Models
{
    [Flags]
    public enum WorkTaskRequiredFieldEnum
    {
        Address     = 0x01,
        Vehicle     = 0x02,
        Link        = 0x03,
        Dolly       = 0x04,
        Trailer     = 0x05
    }
    public enum WorkVehicleTypeEnum
    {
        Semi = 101,
        Truck = 103,
        Lorry = 105,
        Van = 107,
        Trailer = 201,
        Link = 203,
        Dolly = 205
    }
    public enum WorkTaskTypeEnum
    {
        Arrivial = 101,
        Departure = 103,
        PickupGoods = 151,
        DeliverGoods = 153,
        PickupSemi = 201,
        PickupTruck = 203,
        PickupLink = 205,
        PickupDolly = 207,
        PickupTrailer = 209,
        ConnectTrailer = 249,
        ParkSemi = 251,
        ParkTruck = 253,
        ParkLink = 255,
        ParkDolly = 257,
        ParkTrailer = 259,
        DisconnectTrailer = 299,
        Waiting = 301,
        Driverbreak = 303,
        Driverest = 305,
        Refuel = 307,
        CustomerPaperwork = 401,
        Accident = 403,
        Note = 491
    }
}
