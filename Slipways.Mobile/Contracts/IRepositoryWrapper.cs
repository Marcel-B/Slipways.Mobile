using Slipways.Mobile.Data.Models;
using System.Collections.Generic;

namespace Slipways.Mobile.Contracts
{
    public interface IRepositoryWrapper
    {
        ISlipwayRepository Slipways { get; }
        IExtraRepository Extras { get; }
        IManufacturerRepository Manufacturers { get; }
        IMarinaRepository Marinas { get; }
        IServiceRepository Services { get; }
        IStationRepository Stations { get; }
        IWaterRepository Waters { get; }

        IEnumerable<Slipway> GetAllSlipwaysWithSubsets();
    }
}
