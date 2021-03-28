using CC.Minesweeper.Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CC.Minesweeper.Core.Interfaces
{
    public interface IGameRepository : IRepository<Game>
    {
        /// <summary>
        /// Gets a list of games by user id.
        /// </summary>
        /// <param name="userId">The requested user id.</param>
        /// <returns>A list of games.</returns>
        Task<IEnumerable<Game>> GetByUserIdAsync(string userId);

        /// <summary>
        /// Gets a game by user id and game id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="gameId">The game id.</param>
        /// <returns></returns>
        Task<Game> GetByUserIdAndGameIdAsync(string userId, string gameId);
    }
}
