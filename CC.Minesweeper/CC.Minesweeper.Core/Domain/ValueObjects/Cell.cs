namespace CC.Minesweeper.Core.Domain.ValueObjects
{
    /// <summary>
    /// Class representing a cell in the board.
    /// </summary>
    public class Cell
    {
        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public int Value { get; set; }

        public bool IsMine { get; set; }

        public bool IsFlagged { get; set; }

        public bool IsRevealed { get; set; }

        /// <summary>
        /// Sets a mine in the cell.
        /// </summary>
        public void SetMine()
        {
            Value = -1;
            IsMine = true;
        }
    }
}
