namespace tourApp.Model
{
    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System.Linq;

    public class City
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("distances")]
        public Distance[] Distances { get; set; }

        public double distanceTo(City c)
        {
            return Distances.FirstOrDefault(x => x.City == c.Name).Amount;
        }

        public static City[] FromJson(string json) => JsonConvert.DeserializeObject<City[]>(json, Model.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this City[] self) => JsonConvert.SerializeObject(self, Model.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
