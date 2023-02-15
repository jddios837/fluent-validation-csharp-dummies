using FluentValidation.Dummies.Base.Enums;
using FluentValidation.Dummies.Base.Models;

namespace FluentValidation.Dummies.Validation.Models;

public class EmployeeValidator : AbstractValidator<Employee>
{
    public EmployeeValidator()
    {
        // RuleLevelCascadeMode = CascadeMode.Stop; // Will return one VALIDATION per rule (Avoid add .Cascade(CascadeMode.Stop) in each Rule)
        // ClassLevelCascadeMode = CascadeMode.Stop; // Will only return one error VALIDATION per class
        
        RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop) // Return one VALIDATION per rule
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.EmployeeRole)
            .IsInEnum()
            .NotNull();

        When(x => x.EmployeeRole == EmployeeType.NULL, () =>
        {
            RuleFor(x => x.Name).MaximumLength(20);
            RuleFor(x => x.MaxSalary).LessThanOrEqualTo(500);
        });

        // If the child property is null, then the child validator will not be executed.
        // Check this validator with this test Should_Have_Error_NotNull_When_Address_Is_Null
        // RuleForEach(x => x.Addresses)
        //     .SetValidator(new AddressValidator());
        
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