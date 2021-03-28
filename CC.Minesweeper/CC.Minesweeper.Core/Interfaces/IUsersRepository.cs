using CC.Minesweeper.Core.Domain.Entities;
using System.Threading.Tasks;

namespace CC.Minesweeper.Core.Interfaces
{
    public interface IUsersRepository : IRepository<User>
    {
        /// <summary>
        /// Gets a user by email.
        /// </summary>
        /// <param name="email">The requested email.</param>
        /// <returns>A user.</returns>
        Task<User> GetByEmailAsync(string email);

        /// <summary>
        /// Gets a user by email and password.
        /// </summary>
        /// <param name="email">The requested email.</param>
        /// <param name="password">The requested password.</param>
        /// <returns>A user.</returns>
        Task<User> GetByEmailAndPasswordAsync(string email, string password);
    }
}
