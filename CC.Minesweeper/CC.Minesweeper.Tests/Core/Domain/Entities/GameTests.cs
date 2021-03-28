using CC.Minesweeper.Core.Domain.Entities;
using CC.Minesweeper.Core.Domain.ValueObjects;
using CC.Minesweeper.Core.Exceptions;
using Xunit;

namespace CC.Minesweeper.Tests.Core.Domain.Entities
{
    public class GameTests
    {
        [Fact]
        public void ShouldCreateNewGame()
        {
            // Arrange
            var game = new Game();
            int rows = 3;
            int columns = 5;
            int mines = 3;


            // Act
            game.Initialize(3, 5, 3);

            // Assert
            Assert.Equal(GameStatus.InProgress, game.Status);
            Assert.Equal(rows, game.Rows);
            Assert.Equal(columns, game.Columns);
            Assert.Equal(mines, game.Mines);

            int mineCounter = 0;
            int cellCounter = 0;
            int minesWithoutMinesCounter = 0;

            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < columns; y++)
                {
                    if (game.Board[x, y] != null)
                    {
                        cellCounter++;
                    }

                    if(game.Board[x, y].IsMine)
                    {
                        mineCounter++;
                    }
                    else
                    {
                        minesWithoutMinesCounter++;
                    }
                }
            }

            Assert.Equal(mines, mineCounter);
            Assert.Equal(cellCounter, rows * columns);
            Assert.Equal(minesWithoutMinesCounter, (rows * columns) - mines);
        }

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(0, 1, 1)]
        [InlineData(1, 0, 1)]
        [InlineData(1, 1, 0)]
        [InlineData(1, 1, 1)]
        public void ShouldThrowExeptionForInvalidInputs(int rows, int columns, int mines)
        {
            // Arrange
            var game = new Game();

            // Act, Assert
            Assert.Throws<BusinessException>(() => game.Initialize(rows, columns, mines));
        }

        [Fact]
        public void ShouldRevealCell()
        {
            // Arrange
            var game = new Game();
            int rows = 3;
            int columns = 5;

            int xIndex = -1;
            int yIndex = -1; 
            
            game.Initialize(3, 5, 3);

            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < columns; y++)
                {
                    if (!game.Board[x, y].IsMine)
                    {
                        xIndex = x;
                        yIndex = y;
                        break;
                    }
                }
                if (xIndex != -1 && yIndex != -1)
                {
                    break;
                }
            }

            // Act
            game.Reveal(xIndex, yIndex);

            // Assert
            Assert.Equal(GameStatus.InProgress, game.Status);
            Assert.True(game.Board[xIndex, yIndex].IsRevealed);
        }

        [Fact]
        public void ShouldThrowExceptionForAlreadyRevealedCell()
        {
            // Arrange
            var game = new Game();
            int rows = 3;
            int columns = 5;

            int xIndex = -1;
            int yIndex = -1;

            game.Initialize(3, 5, 3);

            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < columns; y++)
                {
                    if (!game.Board[x, y].IsMine)
                    {
                        xIndex = x;
                        yIndex = y;
                        break;
                    }
                }
                if (xIndex != -1 && yIndex != -1)
                {
                    break;
                }
            }

            // Act, Assert
            game.Reveal(xIndex, yIndex);

            Assert.Throws<BusinessException>(() => game.Reveal(xIndex, yIndex));
        }

        [Fact]
        public void ShouldThrowExceptionForRevealRequestOnGameCompleted()
        {
            // Arrange
            var game = new Game();
            int rows = 3;
            int columns = 5;

            int xIndex = -1;
            int yIndex = -1;

            game.Initialize(3, 5, 3);

            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < columns; y++)
                {
                    if (game.Board[x, y].IsMine)
                    {
                        xIndex = x;
                        yIndex = y;
                        break;
                    }
                }
                if (xIndex != -1 && yIndex != -1)
                {
                    break;
                }
            }

            // Act, Assert
            game.Reveal(xIndex, yIndex);

            Assert.Throws<BusinessException>(() => game.Reveal(xIndex, yIndex));
        }

        [Fact]
        public void ShouldFlagCell()
        {
            // Arrange
            var game = new Game();
            int rows = 3;
            int columns = 5;

            int xIndex = -1;
            int yIndex = -1;

            game.Initialize(3, 5, 3);

            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < columns; y++)
                {
                    if (!game.Board[x, y].IsMine)
                    {
                        xIndex = x;
                        yIndex = y;
                        break;
                    }
                }
                if (xIndex != -1 && yIndex != -1)
                {
                    break;
                }
            }

            // Act
            game.SwitchFlag(xIndex, yIndex);

            // Assert
            Assert.Equal(GameStatus.InProgress, game.Status);
            Assert.True(game.Board[xIndex, yIndex].IsFlagged);
        }

        [Fact]
        public void ShouldLose()
        {
            // Arrange
            var game = new Game();
            int rows = 3;
            int columns = 5;

            int xIndex = -1;
            int yIndex = -1;

            game.Initialize(3, 5, 3);

            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < columns; y++)
                {
                    if (game.Board[x, y].IsMine)
                    {
                        xIndex = x;
                        yIndex = y;
                        break;
                    }
                }
                if (xIndex != -1 && yIndex != -1)
                {
                    break;
                }
            }

            // Act
            game.Reveal(xIndex, yIndex);

            // Assert
            Assert.Equal(GameStatus.Failed, game.Status);
        }

        [Fact]
        public void ShouldWin()
        {
            // Arrange
            var game = new Game();
            int rows = 3;
            int columns = 5;

            game.Initialize(3, 5, 3);

            // Act
            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < columns; y++)
                {
                    if (!game.Board[x, y].IsMine)
                    {
                        if (!game.Board[x, y].IsRevealed)
                        {
                            game.Reveal(x, y);
                        }
                    }
                }
            }

            // Assert
            Assert.Equal(GameStatus.Complete, game.Status);
        }
    }
}