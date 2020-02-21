using Newtonsoft.Json;
using SQLite;
using System;

namespace Slipways.Mobile.Data.Models
{
    public class Slipway : IEntity
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string Name { get; set; }

        [JsonProperty("id")]
        public Guid Pk { get; set; }

        public string City { get; set; }

        [Ignore]
        public Water Water { get; set; }

        [JsonIgnore]
        public int WaterId { get => Water.Id; }

        public string Watername => Water.Longname;
    }
}
