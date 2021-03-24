using AutoMapper;
using CC.Minesweeper.Core.Domain.Entities;
using CC.Minesweeper.Core.Interfaces;
using CC.Minesweeper.Infrastructure.Configurations;
using CC.Minesweeper.Infrastructure.Repositories.Entities;
using MongoDB.Driver;
using System.Linq;
using System.Threading.Tasks;

namespace CC.Minesweeper.Infrastructure.Repositories.Repositories
{
    public class UsersRepository : MongoDbRepository<UserDocument, User>, IUsersRepository
    {
        public UsersRepository(
            MongoDbConfiguration mongoDbConfiguration,
            IMapper mapper)
            : base(mongoDbConfiguration, mongoDbConfiguration.UsersCollectionName, mapper)
        {
        }

        public async Task<User> GetByEmailAndPasswordAsync(string email, string password)
        {
            var documents = await Collection.FindAsync(x => x.Email == email && x.Password == password);
            var document = documents.SingleOrDefault();

            return ToEntity(document);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var documents = await Collection.FindAsync(x => x.Email == email);
            var document = documents.SingleOrDefault();

            return ToEntity(document);
        }
    }
}
