using AutoMapper;
using CC.Minesweeper.Api.Controllers.Games.Models;
using CC.Minesweeper.Core.Domain.Entities;
using System;

namespace CC.Minesweeper.Api.Controllers.Games.Mappers
{
    public class GamesProfile : Profile
    {
        public GamesProfile()
        {
            CreateMap<Game, GameResponse>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (GameStatusResponse)Enum.Parse(typeof(GameStatusResponse), src.Status.ToString())))
                .ForMember(dest => dest.Board, opt => opt.MapFrom(src => src.Board));

            CreateMap<Cell, CellResponse>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => GetStatus(src)))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => GetValue(src)));
        }

        private int? GetValue(Cell cell)
        {
            if (cell.IsRevealed)
            {
                return cell.Value;
            }
            else
            {
                return null;
            }
        }

        private CellStatus GetStatus(Cell cell)
        {
            if (cell.IsRevealed)
            {
                return CellStatus.Visible;
            }
            else if (cell.IsFlagged)
            {
                return CellStatus.Flagged;
            }
            else
            {
                return CellStatus.Hidden;
            }
        }
    }
}
