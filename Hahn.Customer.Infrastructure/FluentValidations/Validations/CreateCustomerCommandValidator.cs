using FluentValidation;
using Hahn.Customers.Infrastructure.CQRS.Commands;

namespace Hahn.Customers.Infrastructure.FluentValidations.Validations
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            //CascadeMode = CascadeMode.Stop;
            RuleFor(x => x.FirstName).NotEmpty()
                .WithMessage("First name is a required field.");

            RuleFor(x => x.FirstName).
                MinimumLength(5).MaximumLength(30).WithMessage("First name should be at least 5 characters and maximum should be 30");

            RuleFor(x => x.LastName).NotEmpty()
                .WithMessage("Last Name is a required field.");

            RuleFor(x => x.LastName).
                MinimumLength(5).MaximumLength(30).WithMessage("Last name should be at least 5 characters and maximum should be 30");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required field");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Please enter a valid email");

            RuleFor(x => x.DateOfBirth).LessThanOrEqualTo(DateTime.Now).WithMessage($"Please select a date older than today (${DateTime.Now.ToString("yyyy-MM-dd")})");

            RuleFor(x => x.BankAccountNumber).NotEmpty().WithMessage("Bank account number is required");
            RuleFor(x => x.BankAccountNumber).MinimumLength(12).WithMessage("Bank account number should be at least 12 characters");

        }
    }
}
