using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsicopataPedidos.OrdersManagement.Application.Services.Products
{
    public class ProductRequestDtoValidator : AbstractValidator<ProductRequestDto>
    {
        public ProductRequestDtoValidator()
        {
            RuleFor(product => product.Name)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty.")
                .MaximumLength(100);

            RuleFor(product => product.Description)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty.")
                .MaximumLength(250);

            RuleFor(product => product.Price)
                .NotEmpty()
                .GreaterThan(0)
                .LessThan(decimal.MaxValue);

            RuleFor(product => product.Stock)
                .NotEmpty()
                .GreaterThan(0)
                .LessThan(int.MaxValue);

            RuleFor(product => product.Categories)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty.");
        }
    }
}
