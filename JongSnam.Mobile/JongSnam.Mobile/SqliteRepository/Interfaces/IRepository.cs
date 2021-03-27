using System.Collections.Generic;
using System.Threading.Tasks;
using JongSnam.Mobile.Models;

namespace JongSnam.Mobile.SqliteRepository.Interfaces
{
    /// <summary>
    /// Interface of repository.
    /// </summary>
    /// <typeparam name="T">Generic of entity base model.</typeparam>
    public interface IRepository<T> where T : EntityBase
    {
        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="item">The item of entity {T}.</param>
        /// <returns cref="Task{int}">The number of rows deleted.</returns>
        Task<int> DeleteAsync(T item);

        /// <summary>
        /// Deletes the by synchronize identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns cref="Task{int}">The number of rows deleted.</returns>
        Task<int> DeleteBySyncIdAsync(int id);

        /// <summary>
        /// Gets the by synchronize identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns cref="Task{T}">The entity model result.</returns>
        Task<T> GetBySyncIdAsync(int id);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns cref="Task{List{T}}">The entity models result.</returns>
        Task<List<T>> GetAllAsync();

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="item">The item to insert.</param>
        /// <returns cref="Task{int}">The number of rows inserted.</returns>
        Task<int> InsertAsync(T item);

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="itemsTask">The items task.</param>
        /// <param name="runInTransaction">if set to <c>true</c> [run in transaction].</param>
        /// <returns cref="Task{int}">The number of rows inserted.</returns>
        Task<int> InsertAsync(List<T> itemsTask, bool runInTransaction = true);

        /// <summary>
        /// Deletes all asynchronous.
        /// </summary>
        /// <returns cref="Task{int}">The number of rows deleted.</returns>
        Task<int> DeleteAllAsync();
    }
}