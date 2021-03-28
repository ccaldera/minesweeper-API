using AutoMapper;
using CC.Minesweeper.Core.Domain.Entities;
using CC.Minesweeper.Infrastructure.Repositories.Entities;

namespace CC.Minesweeper.Infrastructure.Mappers
{
    /// <summary>
    /// Maps the documents-related classes.
    /// </summary>
    public class DocumentsProfile : Profile
    {
        public DocumentsProfile()
        {
            CreateMap<User, UserDocument>();
            CreateMap<UserDocument, User>();

            CreateMap<Game, GameDocument>();
            CreateMap<GameDocument, Game>();
        }
    }
}
