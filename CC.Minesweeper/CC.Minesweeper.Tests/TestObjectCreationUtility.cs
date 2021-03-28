using CC.Minesweeper.Core.Domain.Entities;
using System;

namespace CC.Minesweeper.Tests
{
    public static class TestObjectCreationUtility
    {
        public static User CreateUser()
        {
            return new User
            {
                Id = Guid.NewGuid().ToString(),
                Email = "some-email@gmail.com",
                Password = "1234567890",
            };
        }

        public static Game CreateGame()
        {
            var game = new Game();

            game.Initialize(3, 3, 1);

            return game;
        }
    }
}
