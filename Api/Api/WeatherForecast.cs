using Newtonsoft.Json;

namespace api.infomanager.dk
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }
}

namespace api.infomanager.dk.DAL
{
    public partial class Db
    {
        public Db() { }

        //public string Retrieve()
        //{
        //    var obj = new Day();

        //    return JsonConvert.SerializeObject(obj);
        //}
    }
}