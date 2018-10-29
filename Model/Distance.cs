namespace Model
{
    using Newtonsoft.Json;

    public class Distance
    {
        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("distance")]
        public string DistanceText { get; set; }

        public double Amount {get{
            return double.Parse(DistanceText);
        }}
    }
}
