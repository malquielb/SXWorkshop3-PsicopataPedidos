using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsicopataPedidos.OrdersManagement.Application.Services.ShoppingList
{
    public class ShoppingListItemRequestDtoValidator : AbstractValidator<ShoppingListItemRequestDto>
    {
        public ShoppingListItemRequestDtoValidator()
        {
            RuleFor(s => s.ProductId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0)
                .LessThan(int.MaxValue);

            RuleFor(s => s.Quantity)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0)
                .LessThan(int.MaxValue);
        }
    }
}
