using FluentValidation.Dummies.Services.OOP;

namespace FluentValidation.Dummies.Services;

public class FarmService
{
    private Animal _animal = new Cat();

    public FarmService()
    {
        _animal.MakeSound();
    }
}