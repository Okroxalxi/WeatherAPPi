using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace WeatherAPI
{
    class Program
    {
        public static async void jsonSerialiser()
        {
            const string apiKey = "995c5315f51d5cdacb0eb720472f2085";
            HttpClient httpClient = new HttpClient();
            bool looping = true;
            while (looping)
            {
                Console.WriteLine("Enter city name");
                var city = Console.ReadLine();
                Weather weather = new Weather();
                var weatherString = httpClient.GetAsync($"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}")
                    .GetAwaiter().GetResult()
                    .Content.ReadAsStringAsync()
                    .GetAwaiter().GetResult();
                var root = JsonConvert.DeserializeObject<Root>(weatherString);

                Console.WriteLine($"City Name: {root.name} " +
                                  $"Time zone: {(root.timezone) / 3600} " +
                                  $"°C: {Math.Round(root.main.temp_max - 273.15)} " +
                                  $"Feels Like: {Math.Round(root.main.feels_like - 273.15)} " +
                                  $"Wind: {root.wind.speed} " +
                                  $"Humidity: {root.main.humidity} " +
                                  $"Pressure: {root.main.pressure}" + 
                                  $"Sunrise : {root.sys.sunrise} Icon : {weather.icon}");
                Console.WriteLine("Do you want to exit ? ");
                string exitClick = Console.ReadLine();
                if (exitClick == "exit")
                {
                    looping = false;
                }
                else
                {
                    looping = true;
                }
            }

        }

        static void Main(string[] args)
        {
            


            jsonSerialiser();
           
        }
    }

}

public class Root
{
    public Coord coord { get; set; }
    public List<Weather> weather { get; set; }
    public string Base { get; set; }

    public Primary main { get; set; }
    public Wind wind { get; set; }
    public Clouds clouds { get; set; }
    public int dt { get; set; }
    public Sys sys { get; set; }
    public int timezone { get; set; }
    public int id { get; set; }
    public string name { get; set; }
    public int cod { get; set; }
}
public class Coord
{
    public double lon { get; set; }
    public double lat { get; set; }
}

public class Weather
{
    public int id { get; set; }
    public string main { get; set; }
    public string description { get; set; }
    public string icon { get; set; }
}

public class Primary
{
    public double temp { get; set; }
    public double feels_like { get; set; }
    public double temp_min { get; set; }
    public double temp_max { get; set; }
    public int pressure { get; set; }
    public int humidity { get; set; }
}

public class Wind
{
    public double speed { get; set; }
    public double deg { get; set; }
}

public class Clouds
{
    public int all { get; set; }
}

public class Sys
{
    public int type { get; set; }
    public int id { get; set; }
    public double message { get; set; }
    public string country { get; set; }
    public int sunrise { get; set; }
    public int sunset { get; set; }
}


