using Newtonsoft.Json;
using SQLite;
using System;

namespace Slipways.Mobile.Data.Models
{
    public class Water : IEntity
    {

        [JsonIgnore]
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [JsonProperty("Id")]
        public Guid Pk { get; set; }

        public string Longname { get; set; }
    }
}
