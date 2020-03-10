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
        [Indexed]
        public Guid Pk { get; set; }

        public string Longname { get; set; }

        public string Shortname { get; set; }

        public DateTime? Updated { get; set; }
    }
}
