using AutoMapper;
using CC.Minesweeper.Api.Controllers.Users.Models;
using CC.Minesweeper.Core.Domain.Entities;

namespace CC.Minesweeper.Api.Controllers.Users.Mappers
{
    public class UsersMapper : Profile
    {
        public UsersMapper()
        {
            CreateMap<RegistrationRequest, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<User, UserProfileResponse>();
        }
    }
}
