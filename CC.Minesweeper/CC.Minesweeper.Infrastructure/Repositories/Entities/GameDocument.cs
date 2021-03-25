using CC.Minesweeper.Core.Domain.ValueObjects;

namespace CC.Minesweeper.Infrastructure.Repositories.Entities
{
    public class GameDocument : Document
    {
        public string UserId { get; set; }

        public int Rows { get; set; }

        public int Columns { get; set; }

        public int Mines { get; set; }

        public Cell[,] Board { get; set; }

        public GameStatus Status { get; set; }
    }
}
