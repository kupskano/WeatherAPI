using System.Text.Json;
using System.Xml.Serialization;
using WeatherAPI.Models;

namespace WeatherAPI
{
    class Program
    {
        private static readonly string apiKey = "80b52c32c5defe9f4321680510337b52";
        private static readonly HttpClient httpClient = new HttpClient();

        static async Task Main(string[] args)
        {
            Console.WriteLine("Enter a city:");
            string city = Console.ReadLine();

            Console.WriteLine("Do you want JSON or XML format? (Enter 'json' or 'xml')");
            string format = Console.ReadLine()?.ToLower();

            if (format == "json")
            {
                await GetWeatherJson(city);
            }
            else if (format == "xml")
            {
                await GetWeatherXml(city);
            }
            else
            {
                Console.WriteLine("Invalid format.");
            }
        }

        private static async Task GetWeatherJson(string city)
        {
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric&mode=json";

            try
            {
                var response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var jsonData = await response.Content.ReadAsStringAsync();

                // Deserialize JSON to WeatherData object
                var weatherData = JsonSerializer.Deserialize<WeatherModel>(jsonData);

                Console.WriteLine($"City: {weatherData.Name}");
                Console.WriteLine($"Temperature: {weatherData.Main.Temp}°C");
                Console.WriteLine($"Wind Speed: {weatherData.Wind.Speed} m/s");
                Console.WriteLine($"Description: {weatherData.Weather[0].Description}");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Error fetching weather data (JSON) for {city}: {e.Message}");
            }
        }

        private static async Task GetWeatherXml(string city)
        {
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric&mode=xml";

            try
            {
                var response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var xmlData = await response.Content.ReadAsStringAsync();

                // Deserialize XML to WeatherData object
                var weatherData = DeserializeXml<WeatherModel>(xmlData);

                Console.WriteLine($"City: {weatherData.Name}");
                Console.WriteLine($"Temperature: {weatherData.Main.Temp}°C");
                Console.WriteLine($"Wind Speed: {weatherData.Wind.Speed} m/s");
                Console.WriteLine($"Description: {weatherData.Weather[0].Description}");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Error fetching weather data (XML) for {city}: {e.Message}");
            }
        }

        private static T DeserializeXml<T>(string xmlData)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var reader = new System.IO.StringReader(xmlData))
            {
                return (T)serializer.Deserialize(reader);
            }
        }
    }
}
