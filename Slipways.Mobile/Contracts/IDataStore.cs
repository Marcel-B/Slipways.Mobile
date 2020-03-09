using System.Collections.Generic;
using System.Threading.Tasks;
using Slipways.Mobile.Data.Models;

namespace Slipways.Mobile.Contracts
{
    public interface IDataStore
    {
        Task<IEnumerable<Slipway>> GetSlipwaysAsync();
        Task<IEnumerable<Water>> GetWatersAsync();
    }
}
