using AutoMapper;
using CC.Minesweeper.Api.Controllers.Games.Models;
using CC.Minesweeper.Core.Domain.Entities;

namespace CC.Minesweeper.Api.Controllers.Games.Mappers
{
    public class GamesProfile : Profile
    {
        public GamesProfile()
        {
            CreateMap<Game, GameResponse>()
                .ConstructUsing(x => new GameResponse(x))
                .ForAllOtherMembers(x => x.Ignore());
        }
    }
}
