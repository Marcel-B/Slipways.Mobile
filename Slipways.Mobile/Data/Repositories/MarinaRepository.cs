using System.Collections.Generic;
using System.Linq;
using Slipways.Mobile.Contracts;
using Slipways.Mobile.Data.Models;

namespace Slipways.Mobile.Data.Repositories
{
    public class MarinaRepository : BaseRepository<Marina>, IMarinaRepository
    {
        public MarinaRepository(
            IDataContext dataContext) : base(dataContext) { }

        public override List<Marina> GetAll()
        {
            if (Cache != null && Cache.Count > 0)
                return Cache;

            var marinas = base.GetAll();
            var waters = Context.Table<Water>().ToList();
            foreach (var marina in marinas)
            {
                var water = waters.First(_ => _.Pk == marina.WaterPk);
                marina.Water = water;
            }
            Cache = marinas;
            return Cache;
        }
    }
}
