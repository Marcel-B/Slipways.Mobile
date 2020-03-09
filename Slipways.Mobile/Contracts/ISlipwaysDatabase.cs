using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Slipways.Mobile.Data.Models;

namespace Slipways.Mobile.Contracts
{
    public interface ISlipwaysDatabase
    {
        Task<List<T>> GetRecordsAsync<T>() where T : IEntity, new();
        Task<List<T>> GetByQueryAsync<T>(string query) where T : IEntity, new();
        Task<T> GetRecordAsync<T>(int id) where T : IEntity, new();
        Task<T> GetByUuidAsync<T>(Guid uuid) where T : IEntity, new();
        Task<int> SaveRecordAsync<T>(T entity) where T : IEntity, new();
        Task<int> DeleteRecordAsync<T>(T item) where T : IEntity, new();
    }
}
