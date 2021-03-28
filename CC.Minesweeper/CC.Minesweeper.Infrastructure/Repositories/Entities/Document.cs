using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace CC.Minesweeper.Infrastructure.Repositories.Entities
{
    /// <summary>
    /// The base document class.
    /// </summary>
    public abstract class Document
    {
        /// <summary>
        /// Gets or sets the document id.
        /// </summary>
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
