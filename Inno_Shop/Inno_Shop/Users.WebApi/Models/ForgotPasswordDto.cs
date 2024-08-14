using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.Application.Common.Mappings;
using Users.Application.Users.Commands.ForgotPassword;
using Users.Domain;

namespace Users.WebApi.Models
{
    public class ForgotPasswordDto : IMapWith<ForgotPasswordCommand>
    {
        public string Email { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ForgotPasswordDto, ForgotPasswordCommand>()
                .ForMember(userVm => userVm.Email,
                    opt => opt.MapFrom(user => user.Email));
        }
    }

}
