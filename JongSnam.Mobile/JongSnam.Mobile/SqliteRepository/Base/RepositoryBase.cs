using System;
using System.Linq;
using System.Threading.Tasks;
using JongSnam.Mobile.Constants;
using JongSnam.Mobile.Extensions;
using JongSnam.Mobile.Models;
using SQLite;

namespace JongSnam.Mobile.SqliteRepository.Base
{
    /// <summary>
    /// The base repository.
    /// </summary>
    public class RepositoryBase
    {
        /// <summary>
        /// The initialized
        /// </summary>
        private static bool _initialized = false;

        /// <summary>
        /// The lazy initializer
        /// </summary>
        private readonly Lazy<SQLiteAsyncConnection> _lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(SyncDbConstant.DatabasePath, SyncDbConstant.Flags);
        });

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBase"/> class.
        /// </summary>
        public RepositoryBase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        /// <summary>
        /// Gets the database.
        /// </summary>
        /// <value>
        /// The database.
        /// </value>
        protected SQLiteAsyncConnection Database => _lazyInitializer.Value;

        /// <summary>
        /// Initializes the asynchronous.
        /// </summary>
        /// <returns cref="Task">A task to get collection points.</returns>
        private async Task InitializeAsync()
        {
            if (!_initialized)
            {
                await CreateTable<UserModel>();

                _initialized = true;
            }
        }

        /// <summary>
        /// Creates the table.
        /// </summary>
        /// <typeparam name="T">Generic of entity base model.</typeparam>
        /// <returns cref="Task">A task to get collection points.</returns>
        private async Task CreateTable<T>()
        {
            if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(T).Name))
            {
                await Database.CreateTablesAsync(CreateFlags.None, typeof(T)).ConfigureAwait(false);
            }
        }
    }
}
