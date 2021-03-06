using CC.Minesweeper.Core.Domain.ValueObjects;
using System;

namespace CC.Minesweeper.Infrastructure.Repositories.Entities
{
    /// <summary>
    /// The class representing the game in mongo.
    /// </summary>
    public class GameDocument : Document
    {
        public string UserId { get; set; }

        public int Rows { get; set; }

        public int Columns { get; set; }

        public int Mines { get; set; }

        public Cell[,] Board { get; set; }

        public GameStatus Status { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
