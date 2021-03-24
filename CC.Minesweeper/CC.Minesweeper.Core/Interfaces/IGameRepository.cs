using CC.Minesweeper.Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CC.Minesweeper.Core.Interfaces
{
    public interface IGameRepository : IRepository<Game>
    {
        Task<IEnumerable<Game>> GetByUserIdAsync(string userId);
    }
}
