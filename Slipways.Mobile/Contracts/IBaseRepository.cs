using System;
using System.Collections.Generic;

namespace Slipways.Mobile.Contracts
{
    public interface IBaseRepository<T>
    {
        List<T> GetAll();
        List<T> GetByQuery(string query);
        T Get(int id);
        T GetByUuid(Guid uuid);
        int Insert(T entity);
        int Update(int id, T entity);
        int Delete(T item);
    }
}
