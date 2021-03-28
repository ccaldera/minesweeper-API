using System.Threading.Tasks;

namespace CC.Minesweeper.Core.Interfaces
{
    public interface IRepository<IEntity>
    {
        /// <summary>
        /// Gets an item by id.
        /// </summary>
        /// <param name="id">The id to retrieve.</param>
        /// <returns>The requested entity.</returns>
        Task<IEntity> GetAsync(string id);

        /// <summary>
        /// Inserts a new entity.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>
        /// <returns>The entity new id.</returns>
        Task<string> InsertAsync(IEntity entity);

        /// <summary>
        /// Updates an entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>A task.</returns>
        Task UpdateAsync(IEntity entity);

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>A task.</returns>
        Task DeleteAsync(IEntity entity);
    }
}
