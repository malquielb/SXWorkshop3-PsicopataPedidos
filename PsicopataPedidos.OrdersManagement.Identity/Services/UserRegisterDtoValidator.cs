using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsicopataPedidos.OrdersManagement.Identity.Services
{
    public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
    {
        public UserRegisterDtoValidator()
        {
            RuleFor(u => u.FirstName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(100);

            RuleFor(u => u.LastName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(100);

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .EmailAddress();

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MinimumLength(8)
                .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.");
        }
    }
}
