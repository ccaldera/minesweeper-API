using CC.Minesweeper.Core.Domain.Entities;
using System.Threading.Tasks;

namespace CC.Minesweeper.Core.Interfaces
{
    public interface IUsersRepository : IRepository<User>
    {
        Task<User> GetByEmailAsync(string email);

        Task<User> GetByEmailAndPasswordAsync(string email, string password);
    }
}
