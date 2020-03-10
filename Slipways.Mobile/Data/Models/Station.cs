using Newtonsoft.Json;
using Slipways.Mobile.Contracts;
using SQLite;
using System;
namespace Slipways.Mobile.Data.Models
{
    public class Station : IEntity
    {
        [JsonIgnore]
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [JsonProperty("id")]
        public Guid Pk { get; set; }
    }
}
