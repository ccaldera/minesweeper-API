using AutoMapper;
using CC.Minesweeper.Core.Domain.Entities;
using CC.Minesweeper.Core.Interfaces;
using CC.Minesweeper.Infrastructure.Configurations;
using CC.Minesweeper.Infrastructure.Repositories.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CC.Minesweeper.Infrastructure.Repositories.Repositories
{
    /// <inheritdoc/>
    public class GameRepository : MongoDbRepository<GameDocument, Game>, IGameRepository
    {
        public GameRepository(
            MongoDbConfiguration mongoDbConfiguration,
            IMapper mapper)
            : base(mongoDbConfiguration, mongoDbConfiguration.GamesCollectionName, mapper)
        {
        }

        /// <inheritdoc/>
        public async Task<Game> GetByUserIdAndGameIdAsync(string userId, string gameId)
        {
            var documents = await Collection.FindAsync(x => x.UserId == userId && x.Id == gameId);

            var document = documents.SingleOrDefault();

            return ToEntity(document);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Game>> GetByUserIdAsync(string userId)
        {
            var documents = await Collection.FindAsync(x => x.UserId == userId);

            return ToEntities(documents.ToEnumerable());
        }
    }
}
