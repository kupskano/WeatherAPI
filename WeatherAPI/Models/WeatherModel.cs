using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAPI.Models
{
    public class WeatherModel
    {
        public string? Name { get; set; }
        public Main? Main { get; set; }
        public Wind? Wind { get; set; }
        public Weather[]? Weather { get; set; }
    }

    public class Main
    {
        public float Temp { get; set; }
        public float Feels_Like { get; set; }
        public float Temp_Min { get; set; }
        public float Temp_Max { get; set; }
    }//

    public class Wind
    {
        public float Speed { get; set; }
    }

    public class Weather
    {
        public string? Description { get; set; }
    }
}
