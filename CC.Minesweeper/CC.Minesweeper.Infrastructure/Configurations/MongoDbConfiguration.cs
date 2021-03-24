﻿namespace CC.Minesweeper.Infrastructure.Configurations
{
    public class MongoDbConfiguration
    {
        public string DatabaseName { get; set; }

        public string ConnectionString { get; set; }

        public string GamesCollectionName { get; set; }

        public string UsersCollectionName { get; set; }
    }
}