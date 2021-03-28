using CC.Minesweeper.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CC.Minesweeper.Tests.Core.Domain.Entities
{
    public class CellTests
    {
        [Fact]
        public void ShouldSetMine()
        {
            // Arrange
            var cell = new Cell(1, 2);

            // Act
            cell.SetMine();

            // Assert
            Assert.True(cell.IsMine);
        }

        [Fact]
        public void ShouldSetCoordenates()
        {
            // Arrange / Act
            var cell = new Cell(1, 2);

            // Assert
            Assert.Equal(1, cell.X);
            Assert.Equal(2, cell.Y);
        }
    }
}
