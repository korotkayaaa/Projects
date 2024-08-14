using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Common.Mappings;
using Users.Domain;

namespace Users.Application.Users.Queries.GetUsersDetails
{
    public class UserDetailsVm : IMapWith<UserSite>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserSite, UserDetailsVm>()
                .ForMember(userVm => userVm.Name,
                    opt => opt.MapFrom(user => user.Name))
                .ForMember(userVm => userVm.Email,
                    opt => opt.MapFrom(user => user.Email))
                .ForMember(userVm => userVm.Role,
                    opt => opt.MapFrom(user => user.Role))
                .ForMember(userVm => userVm.Password,
                    opt => opt.MapFrom(user => user.Password))
                .ForMember(userVm => userVm.Id,
                    opt => opt.MapFrom(user => user.Id));
        }
    }
}
