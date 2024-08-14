using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Users.Application.Common.Exceptiosn;
using Users.Application.Interfaces;
using Users.Domain;

namespace Users.Application.Users.Commands.ForgotPassword
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand>
    {
        private readonly IUserDbContext _userRepository;
        private readonly IEmailService _emailService;

        public ForgotPasswordCommandHandler(IUserDbContext userRepository, IEmailService emailService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
        }

        public async Task<Unit> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email);
            if (user == null)
            {
                throw new NotFoundException(nameof(UserSite), request.Email);
            }

            var resetToken = GenerateResetToken();
            user.ResetToken = resetToken;
            await _userRepository.UpdateUserAsync(user);

            await _emailService.SendEmailAsync(user.Email, "Password Reset", $"Your reset token is: {resetToken}");

            return Unit.Value;
        }

        private string GenerateResetToken()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }
    }

}
