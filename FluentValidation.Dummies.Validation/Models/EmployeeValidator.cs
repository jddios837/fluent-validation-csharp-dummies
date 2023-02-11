using FluentValidation.Dummies.Base.Enums;
using FluentValidation.Dummies.Base.Models;

namespace FluentValidation.Dummies.Validation.Models;

public class EmployeeValidator : AbstractValidator<Employee>
{
    public EmployeeValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.EmployeeRole)
            .IsInEnum()
            .NotEmpty();

        When(x => x.EmployeeRole == EmployeeType.NULL, () =>
        {
            RuleFor(x => x.Name).MaximumLength(20);
            RuleFor(x => x.MaxSalary).LessThanOrEqualTo(500);
        });

        // If the child property is null, then the child validator will not be executed.
        RuleForEach(x => x.Addresses)
            .NotNull()
            .SetValidator(new AddressValidator())
            .When(x => x.Addresses != null && x.Addresses.Any()); // Even with/without this when 
        // The idea is not use x.Addresses != null
        // because use Expression is always true according to nullable reference types' annotations

        RuleForEach(x => x.Addresses)
            .ChildRules(address =>
            {
                address.RuleFor(x => x.Street)
                    .MaximumLength(100);
            })
                .When(x  => x.EmployeeRole == EmployeeType.ADMIN);
    }
}