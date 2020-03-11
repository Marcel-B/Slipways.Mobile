using Slipways.Mobile.Contracts;
using Slipways.Mobile.Data.Models;
using Slipways.Mobile.Infrastructure;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Slipways.Mobile.Data
{
    public class DataContext : IDataContext
    {
        public SQLiteConnection Context { get; private set; }
        public static bool Initialized = false;

        public TableQuery<T> Table<T>() where T : IEntity, new() => Context.Table<T>();
        public List<T> Query<T>(string query) where T : IEntity, new() => Context.Query<T>(query);
        public int Insert<T>(T entity) where T : IEntity, new() => Context.Insert(entity);
        public int Update<T>(T entity) where T : IEntity, new() => Context.Update(entity);
        public int Delete<T>(T entity) where T : IEntity, new() => Context.Delete<T>(entity);

        public DataContext()
        {
            Context = new SQLiteConnection(Constants.DatabasePath, Constants.Flags);
        }

        public void Initialize()
        {
            if (!Initialized)
            {
                var mappings = Context.TableMappings;

                if (!mappings.Any(m => m.MappedType.Name == typeof(Water).Name))
                    Context.CreateTable<Water>(CreateFlags.None);

                if (!mappings.Any(m => m.MappedType.Name == typeof(Slipway).Name))
                    Context.CreateTable<Slipway>(CreateFlags.None);

                if (!mappings.Any(m => m.MappedType.Name == typeof(Manufacturer).Name))
                    Context.CreateTable<Manufacturer>(CreateFlags.None);

                if (!mappings.Any(m => m.MappedType.Name == typeof(Extra).Name))
                    Context.CreateTable<Extra>(CreateFlags.None);

                if (!mappings.Any(m => m.MappedType.Name == typeof(Marina).Name))
                    Context.CreateTable<Marina>(CreateFlags.None);

                if (!mappings.Any(m => m.MappedType.Name == typeof(Service).Name))
                    Context.CreateTable<Service>(CreateFlags.None);

                if (!mappings.Any(m => m.MappedType.Name == typeof(User).Name))
                {
                    Context.CreateTable<User>(CreateFlags.None);
                    var user = new User
                    {
                        Created = DateTime.Now,
                        Pk = Guid.NewGuid(),
                        Name = "John Doe",
                        Version = "1.0"
                    };
                    Context.Insert(user);
                }
                else
                {

                }
                Initialized = true;
            }
        }
    }
}
