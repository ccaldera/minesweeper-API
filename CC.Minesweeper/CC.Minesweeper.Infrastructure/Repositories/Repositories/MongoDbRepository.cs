using AutoMapper;
using CC.Minesweeper.Core.Domain.Entities;
using CC.Minesweeper.Core.Interfaces;
using CC.Minesweeper.Infrastructure.Configurations;
using CC.Minesweeper.Infrastructure.Repositories.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CC.Minesweeper.Infrastructure.Repositories.Repositories
{
    /// <inheritdoc/>
    public class MongoDbRepository<TDocument, TEntity> : IRepository<TEntity>
        where TDocument : Document
        where TEntity : IEntity
    {
        protected readonly IMongoCollection<TDocument> Collection;
        protected readonly IMapper Mapper;

        public MongoDbRepository(
            MongoDbConfiguration mongoDbConfiguration,
            string collectionName,
            IMapper mapper)
        {
            Mapper = mapper;

            var database = new MongoClient(mongoDbConfiguration.ConnectionString).GetDatabase(mongoDbConfiguration.DatabaseName);

            Collection = database.GetCollection<TDocument>(collectionName);
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(TEntity entity)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, entity.Id);

            await Collection.FindOneAndDeleteAsync(filter);
        }

        /// <inheritdoc/>
        public async Task<TEntity> GetAsync(string id)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
            var documents = await Collection.FindAsync(filter);
            var document = documents.SingleOrDefault();

            var entity = ToEntity(document);

            return entity;
        }

        /// <inheritdoc/>
        public async Task<string> InsertAsync(TEntity entity)
        {
            var document = ToDocument(entity);

            await Collection.InsertOneAsync(document);

            return document.Id;
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(TEntity entity)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, entity.Id);

            var document = ToDocument(entity);

            await Collection.FindOneAndReplaceAsync(filter, document);
        }

        /// <summary>
        /// Maps a document to an entity.
        /// </summary>
        /// <param name="document">The document to map.</param>
        /// <returns>The mapped entity.</returns>
        protected TEntity ToEntity(TDocument document)
        {
            if (document == null)
            {
                return default;
            }

            return Mapper.Map<TDocument, TEntity>(document);
        }

        /// <summary>
        /// Maps a list of documents to an entities.
        /// </summary>
        /// <param name="document">The list of documents to map.</param>
        /// <returns>The mapped entities.</returns>
        protected IEnumerable<TEntity> ToEntities(IEnumerable<TDocument> documents)
        {
            if (documents == null)
            {
                return default;
            }

            return Mapper.Map<IEnumerable<TDocument>, IEnumerable<TEntity>>(documents);
        }

        /// <summary>
        /// Maps an entity to a document.
        /// </summary>
        /// <param name="entity">The entity to map.</param>
        /// <returns>The mapped document.</returns>
        protected TDocument ToDocument(TEntity entity)
        {
            if (entity == null)
            {
                return default;
            }

            return Mapper.Map<TEntity, TDocument>(entity);
        }
    }
}
