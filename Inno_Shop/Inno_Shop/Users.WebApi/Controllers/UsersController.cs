using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.Application.Interfaces;
using Users.Application.Users.Commands.CreateUser;
using Users.Application.Users.Commands.DeleteUser;
using Users.Application.Users.Commands.ForgotPassword;
using Users.Application.Users.Commands.ResetPassword;
using Users.Application.Users.Commands.UpdateUser;
using Users.Application.Users.Queries.GetUserList;
using Users.Application.Users.Queries.GetUsersDetails;
using Users.Persistence;
using Users.WebApi.Models;

namespace Users.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class UsersController:BaseController
    {
        private readonly IMapper _mapper;
        private readonly IUserDbContext _userRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailService _emailSender;

        public UsersController(IMapper mapper, IUserDbContext userRepository, UserManager<IdentityUser> userManager, IEmailService emailSender)
        { _mapper = mapper; _userRepository = userRepository;  _emailSender = emailSender; _userManager = userManager; }
        [HttpGet]
        public async Task<ActionResult<UserListVm>>GetAll()
        {
            var query = new GetUserListQuery
            {

            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetailsVm>> Get(Guid id)
        {
            var query = new GetUsersDetailsQuery
            {
                Id = id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
     
        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateUserDto createUserDto)
        {
            var user = new IdentityUser { UserName = createUserDto.Name, Email = createUserDto.Email };
            var result = await _userManager.CreateAsync(user, createUserDto.Password);
            if (result.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = Url.Action(nameof(ConfirmEmail), "Users", new { token, email = user.Email }, Request.Scheme);
                await _emailSender.SendEmailAsync(user.Email, "Подтверждение email", $"Пожалуйста, подтвердите ваш email, перейдя по <a href='{confirmationLink}'>ссылке</a>.");
                return Ok(user.Id);
            }

            return BadRequest(result.Errors);
        }
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return BadRequest("Invalid email confirmation request.");

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                CreateUserDto createUserDto = new CreateUserDto
                {
                    Name = user.UserName,
                    Email = user.Email,
                    Password = user.PasswordHash
                };
                var command = _mapper.Map<CreateUserCommand>(createUserDto);
                var userId = await Mediator.Send(command);
                return Ok("Email confirmed successfully!");
            }
            return BadRequest("Email confirmation failed.");
        }
        [HttpPut]
        public async Task<ActionResult<Guid>> Update([FromBody] UpdateUserDto updateUserDto)
        {
            var command = _mapper.Map<UpdateUserCommand>(updateUserDto);
                await Mediator.Send(command);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteUserCommand
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        {
            var command = _mapper.Map<ForgotPasswordCommand>(forgotPasswordDto);
            await Mediator.Send(command);
            return NoContent();
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            var command = _mapper.Map<ResetPasswordCommand>(resetPasswordDto);
            await Mediator.Send(command);
            var user = await _userRepository.GetUserByEmailAsync(resetPasswordDto.Email);
            UpdateUserDto updateUserDto = new UpdateUserDto
            {
                Email = user.Email,
                Password = user.Password,
                Name = user.Name,
                Role = "User",
                Id = user.Id
            };
            await Update(updateUserDto);

            return NoContent();
        }

    }
}
