namespace tourApp.Model
{
    using Newtonsoft.Json;
    using System;
    using System.Globalization;

    public class Distance
    {
        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("distance")]
        public string DistanceText { get; set; }

        public double Amount
        {
            get
            {
                return double.Parse(DistanceText, CultureInfo.InvariantCulture);
            }
        }
    }
}
