using CC.Minesweeper.Core.Domain.Entities;
using CC.Minesweeper.Core.Domain.Services;
using CC.Minesweeper.Core.Exceptions;
using CC.Minesweeper.Core.Interfaces;
using CC.Minesweeper.Core.Services;
using Moq;
using Moq.AutoMock;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CC.Minesweeper.Tests.Core.Domain.Services
{
    public class UsersServiceTests
    {
        private readonly AutoMocker mocker = new AutoMocker(MockBehavior.Strict);
        private readonly UsersService service;

        public UsersServiceTests()
        {
            service = mocker.CreateInstance<UsersService>();
        }

        [Fact]
        public async Task ValidateCanLogin()
        {
            // arrange
            var user = TestObjectCreationUtility.CreateUser();

            mocker
                .GetMock<IEncryptionSerice>()
                .Setup(x => x.Encrypt(It.Is<string>(x => x == user.Password)))
                .Returns("some-value");

            mocker
                .GetMock<IUsersRepository>()
                .Setup(x => x.GetByEmailAndPasswordAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(user);

            // act
            var savedUser = await service.LoginAsync(user.Email, user.Password);

            // assert
            Assert.NotNull(savedUser);

            mocker.VerifyAll();
        }

        [Fact]
        public async Task ValidateCanGetUser()
        {
            // arrange
            var user = TestObjectCreationUtility.CreateUser();

            mocker
                .GetMock<IUsersRepository>()
                .Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            // act
            var savedUser = await service.GetUserAsync(user.Email);

            // assert
            Assert.NotNull(savedUser);
            Assert.Null(savedUser.Password);

            mocker.VerifyAll();
        }

        [Fact]
        public async Task ValidateCanCreateUser()
        {
            // arrange
            var user = TestObjectCreationUtility.CreateUser();

            mocker
                .GetMock<IUsersRepository>()
                .Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((User)null);

            mocker
                .GetMock<IEncryptionSerice>()
                .Setup(x => x.Encrypt(It.IsAny<string>()))
                .Returns("some-value");

            mocker
                .GetMock<IUsersRepository>()
                .Setup(x => x.InsertAsync(user))
                .ReturnsAsync("new-id");

            // act
            var createdUser = await service.RegisterUserAsync(user);

            // assert
            Assert.NotNull(createdUser);

            mocker.VerifyAll();
        }

        [Fact]
        public async Task ValidateCreateUserFailsForDuplicatedUser()
        {
            // arrange
            var user = TestObjectCreationUtility.CreateUser();

            mocker
                .GetMock<IUsersRepository>()
                .Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            // act, assert
            await Assert.ThrowsAsync<ResourceAlreadyExistsException>(() => service.RegisterUserAsync(user));

            mocker.VerifyAll();
        }

        [Fact]
        public async Task ValidateFailsOnDeletingUnexistingUser()
        {
            // arrange
            var user = TestObjectCreationUtility.CreateUser();

            mocker
                .GetMock<IUsersRepository>()
                .Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((User)null);

            // act, assert
            await Assert.ThrowsAsync<ResourceNotFoundException>(() => service.DeleteUser(user.Email));

            mocker.VerifyAll();
        }

        [Fact]
        public async Task ValidateCanDeleteUser()
        {
            // arrange
            var user = TestObjectCreationUtility.CreateUser();
            var savedGame = TestObjectCreationUtility.CreateGame();

            var savedStories = new List<Game> { savedGame };

            mocker
                .GetMock<IUsersRepository>()
                .Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            mocker
                .GetMock<IGameRepository>()
                .Setup(x => x.GetByUserIdAsync(It.IsAny<string>()))
                .ReturnsAsync(savedStories);

            mocker
                .GetMock<IUsersRepository>()
                .Setup(x => x.DeleteAsync(It.IsAny<User>()))
                .Returns(Task.CompletedTask);

            mocker
                .GetMock<IGameRepository>()
                .Setup(x => x.DeleteAsync(It.IsAny<Game>()))
                .Returns(Task.CompletedTask);

            // act, assert
            await service.DeleteUser(user.Email);

            mocker.VerifyAll();
        }
    }
}
