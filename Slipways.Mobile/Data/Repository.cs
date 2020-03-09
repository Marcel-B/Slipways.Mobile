using Slipways.Mobile.Contracts;
using Slipways.Mobile.Data.Models;
using Slipways.Mobile.Infrastructure;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Slipways.Mobile.Data
{
    public class Repository : IRepository
    {
        private readonly SQLiteConnection Database = new SQLiteConnection(Constants.DatabasePath, Constants.Flags);
        static bool initialized = false;

        public Repository()
        {
            Initialize();
        }

        private void Initialize()
        {
            if (!initialized)
            {
                var mappings = Database.TableMappings;
                if (!mappings.Any(m => m.MappedType.Name == typeof(Water).Name))
                {
                    Database.CreateTable<Water>(CreateFlags.None);
                }
                if (!mappings.Any(m => m.MappedType.Name == typeof(Slipway).Name))
                {
                    Database.CreateTable<Slipway>(CreateFlags.None);
                }
                if (!mappings.Any(m => m.MappedType.Name == typeof(User).Name))
                {
                    Database.CreateTable<User>(CreateFlags.None);
                    var user = new User
                    {
                        Created = DateTime.Now.ToString(),
                        Pk = Guid.NewGuid(),
                        Name = "Hans"
                    };
                    Database.Insert(user);
                }
                initialized = true;
            }
        }

        public List<T> GetAll<T>() where T : IEntity, new()
            => Database.Table<T>().ToList();

        public List<T> GetByQuery<T>(string query) where T : IEntity, new()
            // SQL queries are also possible
            => Database.Query<T>(query);// "SELECT * FROM [TodoItem] WHERE [Done] = 0"

        public T Get<T>(
            int id) where T : IEntity, new()
            => Database.Table<T>()
            .Where(_ => _.Id == id)
            .FirstOrDefault();

        public T GetByUuid<T>(
            Guid uuid) where T : IEntity, new()
           => Database
            .Table<T>()
            .Where(_ => _.Pk == uuid)
            .FirstOrDefault();

        public int InsertOrUpdate<T>(
            T entity) where T : IEntity, new()
            => entity.Id != 0 ? Database.Update(entity) : Database.Insert(entity);

        public int Delete<T>(
            T item) where T : IEntity, new()
            => Database.Delete(item);
    }
}
