﻿

using FluentValidation;

namespace Application.Customers.Create
{
    public class CreateCustomerCommandValidator: AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty()
                .MaximumLength(50)
                .WithName("First name");

            RuleFor(r => r.LastName)
                .NotEmpty()
                .MaximumLength(50)
                .WithName("Last name");

            RuleFor(r => r.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(255);

            RuleFor(r => r.PhoneNumber)
                .NotEmpty()
                .MaximumLength(15);

            RuleFor(r => r.Country)
                .NotEmpty()
                .MaximumLength(3);

            RuleFor(r => r.Line1)
                .NotEmpty()
                .MaximumLength(20)
                .WithName("Address Line 1");

            RuleFor(r => r.Line2)
                .MaximumLength(20)
                .WithName("Address Line 2");

            RuleFor(r => r.City)
                .NotEmpty()
                .MaximumLength(40);

            RuleFor(r => r.ZipCode)
                .MaximumLength(10);

        }
    }
}
