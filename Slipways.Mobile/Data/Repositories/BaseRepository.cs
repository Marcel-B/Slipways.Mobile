using Slipways.Mobile.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Slipways.Mobile.Data.Repositories
{

    public class BaseRepository<T> : IBaseRepository<T> where T : IEntity, new()
    {
        protected IDataContext Context { get; }
        protected List<T> Cache { get; set; }

        public BaseRepository(
            IDataContext context)
        {
            Context = context;
        }

        public virtual List<T> GetAll()
        {
            if(Cache == null || Cache.Count() == 0)
            {
                Cache = new List<T>();
                Cache = Context.Table<T>().ToList();
            }
            return Cache;
        }

        public List<T> GetByQuery(string query)
            // SQL queries are also possible
            => Context.Query<T>(query);// "SELECT * FROM [TodoItem] WHERE [Done] = 0"

        public T Get(
            int id)
            => Context.Table<T>()
            .Where(_ => _.Id == id)
            .FirstOrDefault();

        public T GetByUuid(
            Guid uuid)
           => Context
            .Table<T>()
            .Where(_ => _.Pk == uuid)
            .FirstOrDefault();

        public int Insert(
            T entity)
            => Context.Insert(entity);

        public int Update(
            int id, T entity)
            => Context.Update(entity);

        public int Delete(
            T item)
            => Context.Delete(item);
    }
}
