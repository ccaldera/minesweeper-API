using CC.Minesweeper.Core.Domain.Entities;
using CC.Minesweeper.Core.Exceptions;
using CC.Minesweeper.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CC.Minesweeper.Core.Domain.Services
{
    public class GameService
    {
        private readonly IGameRepository gameRepository;

        public GameService(
            IGameRepository gameRepository)
        {
            this.gameRepository = gameRepository;
        }

        public async Task<Game> NewGame(string userId, int rows, int columns, int mines)
        {
            var game = new Game();

            game.SetOwner(userId);

            game.Initialize(rows, columns, mines);

            game.Id = await gameRepository.InsertAsync(game);

            return game;
        }

        public async Task<IEnumerable<Game>> GetGames(string userId)
        {
            var games = await gameRepository.GetByUserIdAsync(userId);

            return games.OrderByDescending(x => x.CreationDate);
        }

        public async Task DeleteGame(string userId, string gameId)
        {
            Game game = await GetGame(userId, gameId);

            await gameRepository.DeleteAsync(game);
        }

        private async Task<Game> GetGame(string userId, string gameId)
        {
            var game = await gameRepository.GetByUserIdAndGameIdAsync(userId, gameId);

            if (game == null)
            {
                throw new ResourceNotFoundException($"The game {gameId} was not found");
            }

            return game;
        }

        public async Task<Game> Reveal(string userId, string gameId, int row,int column)
        {
            var game = await GetGame(userId, gameId);

            game.Reveal(row, column);

            await gameRepository.UpdateAsync(game);

            return game;
        }

        public async Task<Game> SwitchFlag(string userId, string gameId, int row, int column)
        {
            var game = await GetGame(userId, gameId);

            game.SwitchFlag(row, column);

            await gameRepository.UpdateAsync(game);

            return game;
        }
    }
}
