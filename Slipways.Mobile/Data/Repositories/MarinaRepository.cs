using Slipways.Mobile.Contracts;
using Slipways.Mobile.Data.Models;

namespace Slipways.Mobile.Data.Repositories
{
    public class MarinaRepository : BaseRepository<Marina>, IMarinaRepository
    {
        public MarinaRepository(IDataContext dataContext) : base(dataContext) { }
    }
}
