using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.Application.Common.Mappings;
using Users.Application.Users.Commands.CreateUser;

namespace Users.WebApi.Models
{
    public class CreateUserDto : IMapWith<CreateUserCommand>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateUserDto, CreateUserCommand>()
                .ForMember(userCommand => userCommand.Name,
                opt => opt.MapFrom(userDto => userDto.Name))
                .ForMember(userCommand => userCommand.Email,
                opt => opt.MapFrom(userDto => userDto.Email))
                .ForMember(userCommand => userCommand.Password,
                opt => opt.MapFrom(userDto => userDto.Password));
        }
    }
}
