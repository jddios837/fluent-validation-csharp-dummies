using FluentValidation.Dummies.Base.Models;

namespace FluentValidation.Dummies.Validation.Models;

public class AddressValidator : AbstractValidator<Address>
{
    public AddressValidator()
    {
        RuleFor(x => x.Street)
            .NotEmpty()
            .MaximumLength(50);
        
        
    }
}