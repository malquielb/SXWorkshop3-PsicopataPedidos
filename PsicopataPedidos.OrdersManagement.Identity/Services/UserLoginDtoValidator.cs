using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsicopataPedidos.OrdersManagement.Identity.Services
{
    public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
    {
        public UserLoginDtoValidator()
        {
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
