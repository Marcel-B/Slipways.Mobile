using System;
using System.Collections.Generic;
using Slipways.Mobile.Data.Models;

namespace Slipways.Mobile.Contracts
{
    public interface IRepository
    {
        List<T> GetAll<T>() where T : IEntity, new();
        List<T> GetByQuery<T>(string query) where T : IEntity, new();
        T Get<T>(int id) where T : IEntity, new();
        T GetByUuid<T>(Guid uuid) where T : IEntity, new();
        int Insert<T>(T entity) where T : IEntity, new();
        int Update<T>(int id, T entity) where T : IEntity, new();
        int Delete<T>(T item) where T : IEntity, new();
    }
}
