using CC.Minesweeper.Core.Domain.ValueObjects;
using CC.Minesweeper.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CC.Minesweeper.Core.Domain.Entities
{
    public class Game : IEntity
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public int Rows { get; set; }

        public int Columns { get; set; }

        public int Mines { get; set; }

        public Cell[,] Board { get; set; }

        public GameStatus Status { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime EndDate { get; set; }

        public void SetOwner(string userId)
        {
            UserId = userId;
        }

        public void Initialize(int rows, int columns, int mines)
        {
            Rows = rows;
            Columns = columns;
            Mines = mines;

            ValidateValues();

            InitializeBoard();

            SetMines();

            SetCounters();

            SetCreationDate();
        }

        public void Reveal(int row, int col)
        {
            if(Status != GameStatus.InProgress)
            {
                throw new BusinessException("This game was already completed");
            }

            var cell = Board[row, col];

            if (cell.IsRevealed)
            {
                throw new BusinessException("Cell already revealed");
            }
            else if (cell.IsFlagged)
            {
                cell.IsFlagged = false;
            }

            cell.IsRevealed = true;

            if (cell.IsMine)
            {
                GameOver();
            }
            else if(cell.Value == 0)
            {
                RevealEmpty(row, col);
            }

            if (Status == GameStatus.InProgress)
            {
                CheckWin();
            }
        }

        public void SwitchFlag(int row, int col)
        {
            if(Status != GameStatus.InProgress)
            {
                throw new BusinessException("This game was already completed");
            }
            var cell = Board[row, col];

            cell.IsFlagged = !cell.IsFlagged;
        }

        private void CheckWin()
        {
            int revealed = 0;

            for (int x = 0; x < Rows; x++)
            {
                for (int y = 0; y < Columns; y++)
                {
                    var cell = Board[x, y];

                    if (cell.IsRevealed)
                    {
                        revealed++;
                    }
                }
            }

            if (revealed == (Rows * Columns) - Mines)
            {
                SetWin();
            }
        }

        public void RevealEmpty(int row, int col)
        {
            var neighbours = GetNeighbours(row, col).Where(x => !x.IsRevealed);

            foreach (var neighbour in neighbours)
            {
                if(!neighbour.IsMine && !neighbour.IsRevealed)
                {
                    neighbour.IsRevealed = true;

                    if (neighbour.Value == 0)
                    {
                        RevealEmpty(neighbour.X, neighbour.Y);
                    }
                }
            }

        }

        private void SetCounters()
        {
            for (int x = 0; x < Rows; x++)
            {
                for (int y = 0; y < Columns; y++)
                {
                    var cell = Board[x, y];

                    if (cell.IsMine)
                    {
                        continue;
                    }

                    var neighbours = GetNeighbours(x, y);

                    cell.Value = neighbours.Count(x => x.IsMine);
                }
            }
        }

        private IEnumerable<Cell> GetNeighbours(int row, int col)
        {
            var neighbours = new List<Cell>();

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (!(i == 0 && j == 0) && CheckLimits(row + i, col + j))
                    {
                        neighbours.Add(Board[row + i, col + j]);
                    }
                }
            }

            return neighbours;
        }

        private bool CheckLimits(int row, int col)
        {
            return row >= 0 && row < Rows && col >= 0 && col < Columns;
        }

        private void SetMines()
        {
            var random = new Random(Guid.NewGuid().GetHashCode());

            int mines = 0;
            while(mines != Mines)
            {
                var x = random.Next(0, Rows);
                var y = random.Next(0, Columns);

                if (!Board[x, y].IsMine)
                {
                    Board[x, y].SetMine();
                    mines++;
                }
            }
        }

        private void InitializeBoard()
        {
            Status = GameStatus.InProgress;

            Board = new Cell[Rows, Columns];

            for (int x = 0; x < Rows; x++)
            {
                for (int y = 0; y < Columns; y++)
                {
                    Board[x, y] = new Cell(x, y);
                }
            }
        }

        private void ValidateValues()
        {
            if (Board != null)
            {
                throw new BusinessException("This game already started.");
            }

            if (Rows < 1)
            {
                throw new BusinessException("Rows must be greater or equal to 1.");
            }

            if (Columns < 1)
            {
                throw new BusinessException("Columns must be greater or equal to 1.");
            }

            if (Mines < 1)
            {
                throw new BusinessException("There must be at least 1 mine in the board.");
            }

            if (Rows * Columns <= Mines)
            {
                throw new BusinessException("The amount of mines must be lower than the number of cells in the board.");
            }
        }

        private void GameOver()
        {
            Status = GameStatus.Failed;

            for (int x = 0; x < Rows; x++)
            {
                for (int y = 0; y < Columns; y++)
                {
                    var cell = Board[x, y];

                    cell.IsRevealed = true;
                }
            }

            SetEndDate();
        }

        private void SetWin()
        {
            Status = GameStatus.Complete;
            SetEndDate();
        }

        private void SetCreationDate() 
        {
            CreationDate = DateTime.UtcNow;
        }

        private void SetEndDate()
        {
            EndDate = DateTime.UtcNow;
        }
    }
}
