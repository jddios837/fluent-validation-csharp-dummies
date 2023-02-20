using FluentAssertions;
using FluentValidation.Dummies.Services.OOP;
using Xunit.Abstractions;

namespace FluentValidation.Dummies.Tests.Services;

public class FarmServiceTest
{
    private Animal _model;
    private readonly ITestOutputHelper _output;

    public FarmServiceTest(ITestOutputHelper output)
    {
        _output = output;
    }
    
    [Fact]
    public void Animal_Is_Cat()
    {
        // Arrange
        _model = new Cat();
        
        // Act
        var result = _model.MakeSound();

        // Assert
        result.Should().Be("!Miiaaauu!");
    }
    
    [Fact]
    public void Animal_Is_Cat_Sleeping()
    {
        // Arrange
        _model = new Cat();
        
        // Act
        var result = _model.Sleep();

        // Assert
        result.Should().Be("ZZzzz miau");
    }
    
    [Fact]
    public void Animal_Is_Cat_GoingBathroom()
    {
        // Arrange
        _model = new Cat();
        
        // Act
        var result = _model.GoBathroom();

        // Assert
        result.Should().Be("Pu miau");
    }

    [Fact]
    public void CheckFactory_Valid()
    {
        // Arrange
        var bag = new Animal[] { new Cat(), new Dog() };
        
        // Act
        foreach (Animal a in bag)
        {
            _output.WriteLine("This animal make sound {0}", a.MakeSound());
        }

        // Assert
        Assert.True(true);
    }
    
}