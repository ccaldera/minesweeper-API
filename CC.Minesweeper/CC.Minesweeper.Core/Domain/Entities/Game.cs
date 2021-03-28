using CC.Minesweeper.Core.Domain.ValueObjects;
using CC.Minesweeper.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CC.Minesweeper.Core.Domain.Entities
{
    /// <summary>
    /// Class that represents a minesweeper game.
    /// </summary>
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

        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Sets user owner of this game.
        /// </summary>
        /// <param name="userId"></param>
        public void SetOwner(string userId)
        {
            UserId = userId;
        }

        /// <summary>
        /// Initializes the game and its cells.
        /// </summary>
        /// <param name="rows">The requested rows.</param>
        /// <param name="columns">The requested columns.</param>
        /// <param name="mines">The requested mines.</param>
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

        /// <summary>
        /// Reveals a cells value.
        /// </summary>
        /// <param name="row">the row axis.</param>
        /// <param name="col">the column axis.</param>
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

        /// <summary>
        /// Marks a cell as flagged.
        /// </summary>
        /// <param name="row">The row axis.</param>
        /// <param name="col">the column axis.</param>
        public void SwitchFlag(int row, int col)
        {
            if(Status != GameStatus.InProgress)
            {
                throw new BusinessException("This game was already completed");
            }
            var cell = Board[row, col];

            cell.IsFlagged = !cell.IsFlagged;
        }

        /// <summary>
        /// Checks if the game was already completed.
        /// </summary>
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

        /// <summary>
        /// Reveals an empty cell.
        /// </summary>
        /// <param name="row">The row axis.</param>
        /// <param name="col">The column axis.</param>
        private void RevealEmpty(int row, int col)
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

        /// <summary>
        /// Sets the numeric values of a cell.
        /// </summary>
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

        /// <summary>
        /// Gets the neighbours of a given cell.
        /// </summary>
        /// <param name="row">The row axis.</param>
        /// <param name="col">The column axis.</param>
        /// <returns>A list of cells surrounding the requested one.</returns>
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

        /// <summary>
        /// Checks the cell boundaries.
        /// </summary>
        /// <param name="row">The row axis.</param>
        /// <param name="col">The column axis.</param>
        /// <returns></returns>
        private bool CheckLimits(int row, int col)
        {
            return row >= 0 && row < Rows && col >= 0 && col < Columns;
        }

        /// <summary>
        /// Sets the requested mines in the board.
        /// </summary>
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

        /// <summary>
        /// Initialices a board.
        /// </summary>
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

        /// <summary>
        /// Validates the requetsed values.
        /// </summary>
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

        /// <summary>
        /// Sets the game state as failed and reveals the rest of the cells.
        /// </summary>
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

        /// <summary>
        /// Sets the game as completed.
        /// </summary>
        private void SetWin()
        {
            Status = GameStatus.Complete;
            SetEndDate();
        }

        /// <summary>
        /// Sets the creation date.
        /// </summary>
        private void SetCreationDate() 
        {
            CreationDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Sets the end date.
        /// </summary>
        private void SetEndDate()
        {
            EndDate = DateTime.UtcNow;
        }
    }
}
