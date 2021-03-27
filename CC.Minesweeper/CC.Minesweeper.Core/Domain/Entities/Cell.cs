namespace CC.Minesweeper.Core.Domain.Entities
{
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

        public void SetMine()
        {
            Value = -1;
            IsMine = true;
        }
    }
}
