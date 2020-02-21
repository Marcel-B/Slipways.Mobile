using Slipways.Mobile.Data.Models;
using Slipways.Mobile.Infrastructure;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slipways.Mobile.Data
{
    public class SlipwaysDatabase
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public SlipwaysDatabase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Water).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(Water)).ConfigureAwait(false);
                }
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Slipway).Name))
                {
                    await Database.CreateTableAsync<Slipway>().ConfigureAwait(false);
                }
                initialized = true;
            }
        }
        public async Task<List<T>> GetRecordsAsync<T>() where T : IEntity, new()
            => await Database.Table<T>().ToListAsync();

        public Task<List<Slipway>> GetItemsNotDoneAsync()
        {
            // SQL queries are also possible
            return Database.QueryAsync<Slipway>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }

        public async Task<T> GetRecordAsync<T>(
            int id) where T : IEntity, new()
            => await Database.Table<T>()
            .Where(_ => _.Id == id)
            .FirstOrDefaultAsync();

        public async Task<T> GetByUuidAsync<T>(
            Guid uuid) where T : IEntity, new()
           => await Database.Table<T>().Where(_ => _.Pk == uuid).FirstOrDefaultAsync();

        public async Task<int> SaveRecordAsync<T>(
            T entity) where T : IEntity, new()
            => entity.Id != 0 ? await Database.UpdateAsync(entity) : await Database.InsertAsync(entity);

        public async Task<int> DeleteRecordAsync<T>(
            T item) where T : IEntity, new()
            => await Database.DeleteAsync(item);
    }
}
