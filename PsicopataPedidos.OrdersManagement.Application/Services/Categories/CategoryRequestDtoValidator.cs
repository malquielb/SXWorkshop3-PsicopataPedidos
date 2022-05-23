using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsicopataPedidos.OrdersManagement.Application.Services.Categories
{
    public class CategoryRequestDtoValidator : AbstractValidator<CategoryRequestDto>
    {
        public CategoryRequestDtoValidator()
        {
            RuleFor(category => category.Name)
                .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty.");
        }
    }
}
