using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Users.Application.Interfaces;

namespace Users.Application.Users.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand>
    {
        private readonly IUserDbContext _userRepository;

        public ResetPasswordCommandHandler(IUserDbContext userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email);
            if (user == null || user.ResetToken != request.Token)
            {
                throw new UnauthorizedAccessException("Invalid token");
            }

            user.Password = HashPassword(request.NewPassword);
            user.ResetToken = null;

            await _userRepository.UpdateUserAsync(user);

            return Unit.Value;
        }

        private string HashPassword(string password)
        {
            return password;
        }
    }
}
