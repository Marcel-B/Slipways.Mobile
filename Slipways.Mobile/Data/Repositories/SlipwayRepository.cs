using Slipways.Mobile.Contracts;
using Slipways.Mobile.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace Slipways.Mobile.Data.Repositories
{
    public class SlipwayRepository : BaseRepository<Slipway>, ISlipwayRepository
    {
        public SlipwayRepository(
            IDataContext dataContext) : base(dataContext)
        {
        }

        public override List<Slipway> GetAll()
        {
            if (Cache != null && Cache.Count > 0)
                return Cache;

            var slipways = base.GetAll();
            var waters = Context.Table<Water>().ToList();
            var marinas = Context.Table<Marina>().ToList();
            foreach (var slipway in slipways)
            {
                var water = waters.First(_ => _.Pk == slipway.WaterPk);
                slipway.Water = water;
                var marina = marinas.FirstOrDefault(_ => _.Pk == slipway.MarinaPk);

                if (marina != null)
                    slipway.Marina = marina;
            }
            Cache = slipways;
            return Cache;
        }
    }
}

