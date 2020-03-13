using Prism.Events;
using Slipways.Mobile.Contracts;
using Slipways.Mobile.Data.Models;
using Slipways.Mobile.Events;
using Slipways.Mobile.Infrastructure;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slipways.Mobile.Data
{
    public class DataContext : IDataContext
    {
        public SQLiteAsyncConnection Context { get; }
        private readonly InitializationReadyEvent _initializationReadyEvent;

        private bool _initialized = false;
        public AsyncTableQuery<T> Table<T>() where T : IEntity, new() => Context.Table<T>();
        public Task<List<T>> QueryAsync<T>(string query) where T : IEntity, new() => Context.QueryAsync<T>(query);
        public Task<int> InsertAsync<T>(T entity) where T : IEntity, new() => Context.InsertAsync(entity);
        public Task<int> UpdateAsync<T>(T entity) where T : IEntity, new() => Context.UpdateAsync(entity);
        public Task<int> DeleteAsync<T>(T entity) where T : IEntity, new() => Context.DeleteAsync<T>(entity);

        public DataContext(
            IEventAggregator eventAggregator)
        {
            Context = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            _initializationReadyEvent = eventAggregator.GetEvent<InitializationReadyEvent>();
        }

        public async Task<bool> Initialize()
        {
            if (_initialized)
            {
                var mappings = Context.TableMappings;

                //if (!mappings.Any(m => m.MappedType.Name == typeof(Water).Name))
                await Context.CreateTableAsync<Water>(CreateFlags.None);

                //if (!mappings.Any(m => m.MappedType.Name == typeof(Slipway).Name))
                await Context.CreateTableAsync<Slipway>(CreateFlags.None);

                //if (!mappings.Any(m => m.MappedType.Name == typeof(Manufacturer).Name))
                await Context.CreateTableAsync<Manufacturer>(CreateFlags.None);

                //if (!mappings.Any(m => m.MappedType.Name == typeof(Extra).Name))
                await Context.CreateTableAsync<Extra>(CreateFlags.None);

                //if (!mappings.Any(m => m.MappedType.Name == typeof(Marina).Name))
                await Context.CreateTableAsync<Marina>(CreateFlags.None);

                //if (!mappings.Any(m => m.MappedType.Name == typeof(Service).Name))
                await Context.CreateTableAsync<Service>(CreateFlags.None);

                //if (!mappings.Any(m => m.MappedType.Name == typeof(Station).Name))
                await Context.CreateTableAsync<Station>(CreateFlags.None);

                if (!mappings.Any(m => m.MappedType.Name == typeof(User).Name))
                {
                    await Context.CreateTableAsync<User>(CreateFlags.None);
                    var user = new User
                    {
                        Created = DateTime.Now,
                        Pk = Guid.NewGuid(),
                        Name = "John Doe",
                        Version = "1.0"
                    };
                    await Context.InsertAsync(user);
                }
                else
                {

                }
                _initialized = true;
            }
            // Database now ready - check for updates
            _initializationReadyEvent.Publish(_initialized);
            return _initialized;
        }
    }
}
