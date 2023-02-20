namespace FluentValidation.Dummies.Services.OOP;

public class Cat : Animal
{
    public override string MakeSound()
    {
        Console.WriteLine("!Miiaaauu!");
        return "!Miiaaauu!";
    }

    public override string GoBathroom()
    {
        return "Pu miau";
    }
}