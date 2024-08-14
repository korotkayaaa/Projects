using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.Application.Common.Mappings;
using Users.Application.Users.Commands.UpdateUser;

namespace Users.WebApi.Models
{
    public class UpdateUserDto : IMapWith<UpdateUserCommand>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateUserDto, UpdateUserCommand>()
               .ForMember(userCommand => userCommand.Name,
               opt => opt.MapFrom(userDto => userDto.Name))
                  .ForMember(userCommand => userCommand.Id,
               opt => opt.MapFrom(userDto => userDto.Id))
                        .ForMember(userCommand => userCommand.Role,
               opt => opt.MapFrom(userDto => userDto.Role))
                           .ForMember(userCommand => userCommand.Password,
               opt => opt.MapFrom(userDto => userDto.Password));
        }
    }
}
