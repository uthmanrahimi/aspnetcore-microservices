using FluentValidation;

using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
   public class UpdateOrderValidator:AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("{UserName} is required.")
               .NotNull().MaximumLength(50).WithMessage("{UserName} must not exceed 50 characters");

            RuleFor(x => x.EmailAddress).NotEmpty().WithMessage("{EmailAddress} is required.");

            RuleFor(x => x.TotalPrice).NotEmpty().WithMessage("{TotalPrice} is required")
                .GreaterThan(0).WithMessage("{TotalPrice} must be greater than zero.");
        }
    }
}
