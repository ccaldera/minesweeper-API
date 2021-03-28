namespace CC.Minesweeper.Infrastructure.Configurations
{
    /// <summary>
    /// Configurations for mongoDb connections.
    /// </summary>
    public class MongoDbConfiguration
    {
        /// <summary>
        /// Gets or sets the database name.
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the games collection name.
        /// </summary>
        public string GamesCollectionName { get; set; }

        /// <summary>
        /// Gets or sets the users collection name.
        /// </summary>
        public string UsersCollectionName { get; set; }
    }
}
