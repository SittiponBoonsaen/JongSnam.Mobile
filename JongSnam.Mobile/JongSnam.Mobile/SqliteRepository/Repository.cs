using System.Collections.Generic;
using System.Threading.Tasks;
using JongSnam.Mobile.Models;
using JongSnam.Mobile.SqliteRepository.Base;
using JongSnam.Mobile.SqliteRepository.Interfaces;

namespace JongSnam.Mobile.SqliteRepository
{
    /// <summary>
    /// Implement generic repository.
    /// </summary>
    /// <typeparam name="T">Generic of entity base model.</typeparam>
    /// <seealso cref="Kob.Uco.Mobile.Repositories.Base.RepositoryBase" />
    /// <seealso cref="Kob.Uco.Mobile.Repositories.Interfaces.IRepository{T}" />
    public class Repository<T> : RepositoryBase, IRepository<T> where T : EntityBase, new()
    {
        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns cref="Task{List{T}}">The entity models result.</returns>
        public Task<List<T>> GetAllAsync()
        {
            return Database.Table<T>().ToListAsync();
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns cref="Task{T}">The entity model result.</returns>
        public Task<T> GetBySyncIdAsync(int id)
        {
            return Database.Table<T>().Where(i => i.SyncId == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="item">The item to delete.</param>
        /// <returns cref="Task{int}">The number of rows inserted.</returns>
        public Task<int> InsertAsync(T item)
        {
            return Database.InsertAsync(item);
        }

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="runInTransaction">if set to <c>true</c> [run in transaction].</param>
        /// <returns cref="Task{int}">The number of rows inserted.</returns>
        public Task<int> InsertAsync(List<T> items, bool runInTransaction = true)
        {
            return Database.InsertAllAsync(items, runInTransaction);
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="item">The item to delete.</param>
        /// <returns cref="Task{int}">The number of rows deleted.</returns>
        public Task<int> DeleteAsync(T item)
        {
            return Database.DeleteAsync(item);
        }

        /// <summary>
        /// Deletes the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns cref="Task{int}">The number of rows deleted.</returns>
        public Task<int> DeleteBySyncIdAsync(int id)
        {
            return Database.DeleteAsync<T>(id);
        }

        /// <summary>
        /// Deletes all asynchronous.
        /// </summary>
        /// <returns cref="Task{int}">The number of rows deleted.</returns>
        public Task<int> DeleteAllAsync()
        {
            return Database.DeleteAllAsync<T>();
        }
    }
}