using CC.Minesweeper.Core.Domain.Entities;
using CC.Minesweeper.Core.Exceptions;
using CC.Minesweeper.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CC.Minesweeper.Core.Domain.Services
{
    /// <summary>
    /// Class containing the game service operations.
    /// </summary>
    public class GameService
    {
        private readonly IGameRepository gameRepository;

        public GameService(
            IGameRepository gameRepository)
        {
            this.gameRepository = gameRepository;
        }

        /// <summary>
        /// Creates a new game.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="rows">The requested rows.</param>
        /// <param name="columns">The requested columns.</param>
        /// <param name="mines">The requested mines.</param>
        /// <returns>Returns a band new game.</returns>
        public async Task<Game> NewGame(string userId, int rows, int columns, int mines)
        {
            var game = new Game();

            game.SetOwner(userId);

            game.Initialize(rows, columns, mines);

            game.Id = await gameRepository.InsertAsync(game);

            return game;
        }

        /// <summary>
        /// Gets the games related to a user.
        /// </summary>
        /// <param name="userId">The requedted user id.</param>
        /// <returns>A list of games.</returns>
        public async Task<IEnumerable<Game>> GetGames(string userId)
        {
            var games = await gameRepository.GetByUserIdAsync(userId);

            return games.OrderByDescending(x => x.CreationDate);
        }

        /// <summary>
        /// Deletes a game.
        /// </summary>
        /// <param name="userId">The requested user id.</param>
        /// <param name="gameId">The requested game id.</param>
        /// <returns>A task.</returns>
        public async Task DeleteGame(string userId, string gameId)
        {
            Game game = await GetGame(userId, gameId);

            await gameRepository.DeleteAsync(game);
        }

        /// <summary>
        /// Reveals a cell and updates the game status.
        /// </summary>
        /// <param name="userId">The requested user id.</param>
        /// <param name="gameId">The requested game id.</param>
        /// <param name="row">The requested row axis.</param>
        /// <param name="column">The requested column axis.</param>
        /// <returns>The updated game.</returns>
        public async Task<Game> Reveal(string userId, string gameId, int row,int column)
        {
            var game = await GetGame(userId, gameId);

            game.Reveal(row, column);

            await gameRepository.UpdateAsync(game);

            return game;
        }

        /// <summary>
        /// Switch the flag state for a cell and updates the game status.
        /// </summary>
        /// <param name="userId">The requested user id.</param>
        /// <param name="gameId">The requested game id.</param>
        /// <param name="row">The requested row axis.</param>
        /// <param name="column">The requested column axis.</param>
        /// <returns>The updated game.</returns>
        public async Task<Game> SwitchFlag(string userId, string gameId, int row, int column)
        {
            var game = await GetGame(userId, gameId);

            game.SwitchFlag(row, column);

            await gameRepository.UpdateAsync(game);

            return game;
        }

        /// <summary>
        /// Gets a game by its user id and game id.
        /// </summary>
        /// <param name="userId">The requested user id.</param>
        /// <param name="gameId">The requested game id.</param>
        /// <returns>The game.</returns>
        private async Task<Game> GetGame(string userId, string gameId)
        {
            var game = await gameRepository.GetByUserIdAndGameIdAsync(userId, gameId);

            if (game == null)
            {
                throw new ResourceNotFoundException($"The game {gameId} was not found");
            }

            return game;
        }

    }
}
