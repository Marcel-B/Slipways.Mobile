using SQLite;
using System.Collections.Generic;

namespace Slipways.Mobile.Contracts
{
    public interface IDataContext
    {
        SQLiteConnection Context { get; }
        TableQuery<T> Table<T>() where T : IEntity, new();
        List<T> Query<T>(string query) where T : IEntity, new();
        int Insert<T>(T entity) where T : IEntity, new();
        int Update<T>(T entity) where T : IEntity, new();
        int Delete<T>(T entity) where T : IEntity, new();
        public void Initialize();
    }
}
