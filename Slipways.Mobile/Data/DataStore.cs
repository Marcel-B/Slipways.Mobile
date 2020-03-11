using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prism.Events;
using Slipways.Mobile.Contracts;
using Slipways.Mobile.Data.Models;
using Slipways.Mobile.Events;

namespace Slipways.Mobile.Data
{
    public class DataStore : IDataStore
    {
        private IDataContext _context;
        private IEventAggregator _eventAggregator;
        private IRepositoryWrapper _repository;
        private IUpdateService _updateService;

        public IEnumerable<Water> Waters => _repository.Waters.GetAll();
        public IEnumerable<Slipway> Slipways => _repository.Slipways.GetAll();
        public IEnumerable<Manufacturer> Manufacturers => _repository.Manufacturers.GetAll();
        public IEnumerable<Extra> Extras => _repository.Extras.GetAll();
        public IEnumerable<Marina> Marinas => _repository.Marinas.GetAll();
        public IEnumerable<Service> Services => _repository.Services.GetAll();

        public DataStore(
            IDataContext context,
            IEventAggregator eventAggregator,
            IRepositoryWrapper repository,
            IUpdateService updateService)
        {
            _context = context;
            _eventAggregator = eventAggregator;
            _repository = repository;
            _updateService = updateService;
        }

        /// <summary>
        /// Load Data from SQLite Database to Memory
        /// </summary>
        public async Task LoadData()
        {
            if (!DataContext.Initialized)
                _context.Initialize();

            var waters = Waters;
            if (waters == null || waters.Count() == 0)
                await _updateService
                    .UpdateWater()
                    .ConfigureAwait(false);

            var slipways = Slipways;
            if (slipways == null || slipways.Count() == 0)
                await _updateService
                    .UpdateSlipway()
                    .ConfigureAwait(false);

            var manufacturers = Manufacturers;
            if (manufacturers == null || manufacturers.Count() == 0)
                await _updateService
                    .UpdateManufacturer()
                    .ConfigureAwait(false);

            var marinas = Marinas;
            if (marinas == null || marinas.Count() == 0)
                await _updateService
                    .UpdateMarina()
                    .ConfigureAwait(false);

            _eventAggregator.GetEvent<UpdateReadyEvent>().Publish("rdy");
        }
    }
}