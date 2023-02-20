namespace FluentValidation.Dummies.Services.OOP;

public abstract class Animal: INeeds
{
    public abstract string MakeSound();
    
    // Regular Method
    public void SleepBase()
    {
        Console.WriteLine("ZZzz base");
    }
    
    // Virtual Method
    public virtual string Sleep()
    {
        Console.WriteLine("ZZzzz abstract");
        return "ZZzzz abstract";
    }

    public virtual string GoBathroom()
    {
        Console.WriteLine("Puu abstract");
        return "Pu abstract";
    }
}