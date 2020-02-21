using System;

namespace Slipways.Mobile.Data.Models
{
    public interface IEntity
    {
        Guid Pk { get; set; }
        int Id { get; set; }
    }
}
