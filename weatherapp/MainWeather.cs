// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
using System;
using System.Collections.Generic;

public class WeatherDetails
{
    public int id { get; set; }
    public string description { get; set; }
    public double temp { get; set; }
    public double temp_min { get; set; }
    public double temp_max { get; set; }
    public DateTime forecast_datetime { get; set; }
    public long dt => ((DateTimeOffset)forecast_datetime).ToUnixTimeSeconds();
}

public class Root
{
    public List<WeatherDetails> list { get; set; }
}

