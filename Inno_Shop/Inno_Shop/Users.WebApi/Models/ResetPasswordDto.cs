using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.Application.Common.Mappings;
using Users.Application.Users.Commands.ResetPassword;

namespace Users.WebApi.Models
{
    public class ResetPasswordDto : IMapWith<ResetPasswordCommand>
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ResetPasswordDto, ResetPasswordCommand>()
                .ForMember(userVm => userVm.Email,
                    opt => opt.MapFrom(user => user.Email))
                .ForMember(userVm => userVm.Token,
                    opt => opt.MapFrom(user => user.Token))
                .ForMember(userVm => userVm.NewPassword,
                    opt => opt.MapFrom(user => user.NewPassword));
        }
    }

}
