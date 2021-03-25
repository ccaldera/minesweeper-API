namespace CC.Minesweeper.Core.Domain.ValueObjects
{
    public class Cell
    {
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
