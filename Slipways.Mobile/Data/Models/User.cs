using System;
using SQLite;

namespace Slipways.Mobile.Data.Models
{
    public class User : IEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public Guid Pk { get; set; }
        public string Name { get; set; }
        public string Created { get; set; }
        public string Version { get; set; }
    }
}
