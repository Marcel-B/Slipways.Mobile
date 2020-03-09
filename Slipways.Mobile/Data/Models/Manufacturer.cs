using System;
using Newtonsoft.Json;
using SQLite;

namespace Slipways.Mobile.Data.Models
{
    public class Manufacturer : IEntity
    {
        [JsonIgnore]
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        [JsonProperty("id")]
        public Guid Pk { get; set; }
    }
}
