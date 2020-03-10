using Newtonsoft.Json;
using SQLite;
using System;

namespace Slipways.Mobile.Data.Models
{
    public class Slipway : IEntity
    {
        [JsonIgnore]
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        [JsonProperty("id")]
        [Indexed]
        public Guid Pk { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string Postalcode { get; set; }

        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        [JsonIgnore]
        public Guid WaterPk { get; set; }

        [Ignore]
        public Water Water { get; set; }

        [JsonIgnore]
        public int WaterId { get => Water.Id; }

        public string Watername => Water.Longname;
    }
}
