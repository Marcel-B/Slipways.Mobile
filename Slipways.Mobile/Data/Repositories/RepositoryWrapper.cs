using Slipways.Mobile.Contracts;
using Slipways.Mobile.Data.Models;
using System.Collections.Generic;

namespace Slipways.Mobile.Data.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        public ISlipwayRepository Slipways { get; }
        public IExtraRepository Extras { get; }
        public IWaterRepository Waters { get; }
        public IManufacturerRepository Manufacturers { get; }
        public IStationRepository Stations { get; }
        public IMarinaRepository Marinas { get; }
        public IServiceRepository Services { get; }

        protected readonly IDataContext Context;

        public RepositoryWrapper(
            IDataContext context,
            ISlipwayRepository slipwaysRepository,
            IWaterRepository waterRepository,
            IManufacturerRepository manufacturerRepository,
            IServiceRepository serviceRepository,
            IMarinaRepository marinaRepository,
            IStationRepository stationRepository,
            IExtraRepository extraRepository)
        {
            Context = context;
            Slipways = slipwaysRepository;
            Stations = stationRepository;
            Marinas = marinaRepository;
            Services = serviceRepository;
            Waters = waterRepository;
            Manufacturers = manufacturerRepository;
            Extras = extraRepository;
        }

        public IEnumerable<Slipway> GetAllSlipwaysWithSubsets()
        {
            return new Slipway[0];
        }
    }
}
