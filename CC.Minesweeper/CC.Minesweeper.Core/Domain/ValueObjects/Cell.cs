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

        /// <summary>
        /// Gets or sets the X axis value.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Gets or sets the Y axis value.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Gets or sets the cell value.
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Gets or sets whether a cell is a mine or not.
        /// </summary>
        public bool IsMine { get; set; }

        /// <summary>
        /// Gets or sets whether a cell is flagged or not.
        /// </summary>
        public bool IsFlagged { get; set; }

        /// <summary>
        /// Gets or sets whether a cell is revealed or not.
        /// </summary>
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
