using CC.Minesweeper.Core.Domain.Entities;
using CC.Minesweeper.Core.Exceptions;
using CC.Minesweeper.Core.Interfaces;
using CC.Minesweeper.Core.Services;
using System.Linq;
using System.Threading.Tasks;

namespace CC.Minesweeper.Core.Domain.Services
{
    /// <summary>
    /// Class containing the user service operations.
    /// </summary>
    public class UsersService
    {
        private readonly IUsersRepository usersRepository;
        private readonly IEncryptionSerice encryptionSerice;
        private readonly IGameRepository gameRepository;

        public UsersService(
            IUsersRepository usersRepository,
            IEncryptionSerice encryptionSerice,
            IGameRepository gameRepository)
        {
            this.encryptionSerice = encryptionSerice;
            this.usersRepository = usersRepository;
            this.gameRepository = gameRepository;
        }

        /// <summary>
        /// Validates username and password.
        /// </summary>
        /// <param name="username">The requested username.</param>
        /// <param name="password">The requested password.</param>
        /// <returns>The user.</returns>
        public async Task<User> LoginAsync(string username, string password)
        {
            var encryptedPassword = encryptionSerice.Encrypt(password);

            var user = await usersRepository.GetByEmailAndPasswordAsync(username, encryptedPassword);

            return user;
        }

        /// <summary>
        /// Gets a user by email.
        /// </summary>
        /// <param name="email">The requested email.</param>
        /// <returns>The user.</returns>
        public async Task<User> GetUserAsync(string email)
        {
            var user = await usersRepository.GetByEmailAsync(email);

            user.Password = null;

            return user;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="newUser">The requested new user.</param>
        /// <returns>The new user.</returns>
        public async Task<User> RegisterUserAsync(User newUser)
        {
            var existingUser = await usersRepository.GetByEmailAsync(newUser.Email);

            if (existingUser != null)
            {
                throw new ResourceAlreadyExistsException($"There is already a user register for email {newUser.Email}");
            }

            newUser.Password = encryptionSerice.Encrypt(newUser.Password);

            await usersRepository.InsertAsync(newUser);

            return newUser;
        }

        /// <summary>
        /// Deletes a user by email.
        /// </summary>
        /// <param name="email">The requested email.</param>
        /// <returns>A task.</returns>
        public async Task DeleteUser(string email)
        {
            var user = await usersRepository.GetByEmailAsync(email);

            if (user == null)
            {
                throw new ResourceNotFoundException($"The requested user '{email}' does not exists.");
            }

            var savedStories = await gameRepository.GetByUserIdAsync(user.Id);

            var deleteTasks = savedStories.Select(savedStory => gameRepository.DeleteAsync(savedStory));

            await Task.WhenAll(deleteTasks);

            await usersRepository.DeleteAsync(user);
        }
    }
}
