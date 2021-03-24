using System.Threading.Tasks;

namespace CC.Minesweeper.Core.Interfaces
{
    public interface IRepository<IEntity>
    {
        Task<IEntity> GetAsync(string id);

        Task<string> InsertAsync(IEntity entity);

        Task UpdateAsync(IEntity entity);

        Task DeleteAsync(IEntity entity);
    }
}
