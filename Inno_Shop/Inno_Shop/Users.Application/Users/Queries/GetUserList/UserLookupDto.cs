using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Common.Mappings;
using Users.Domain;

namespace Users.Application.Users.Queries.GetUserList
{
    public class UserLookupDto : IMapWith<UserSite>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserSite, UserLookupDto>()
                .ForMember(userDto => userDto.Id,
                    opt => opt.MapFrom(user => user.Id))
                .ForMember(userDto => userDto.Name,
                    opt => opt.MapFrom(user => user.Name))
                .ForMember(userDto => userDto.Email,
                    opt => opt.MapFrom(user => user.Email));
        }
    }
}
