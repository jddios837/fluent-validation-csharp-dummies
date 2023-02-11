using FluentValidation.Dummies.Base.Enums;
using FluentValidation.Dummies.Base.Models;
using FluentValidation.Dummies.Validation.Models;
using FluentValidation.TestHelper;

namespace FluentValidation.Dummies.Tests.Models;

public class EmployeeValidatorTest
{
    private readonly EmployeeValidator _validator;
    private readonly Employee _model;

    public EmployeeValidatorTest()
    {
        _validator = new EmployeeValidator();
        _model = new Employee();
    }

    [Fact]
    public void Should_Have_Error_Empty_Name()
    {
        // Arrange
        _model.Name = "";
        
        // Act
        var result = _validator.TestValidate(_model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }
    
    [Fact]
    public void Should_Have_Error_MaxLenght_Name()
    {
        // Arrange
        _model.Name = new string('c', 51);
        
        // Act
        var result = _validator.TestValidate(_model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }
    
    [Fact]
    public void Should_Not_Have_Error_MaxLenght_Name()
    {
        // Arrange
        _model.Name = new string('c', 45);
        
        // Act
        var result = _validator.TestValidate(_model);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Name);
    }

    [Theory]
    [MemberData(nameof(EnumValues))]
    public void Should_Not_Have_Error_Enum_EmployeeRole(EmployeeType value)
    {
        // Arrange
        _model.EmployeeRole = value;
        
        // Act
        var result = _validator.TestValidate(_model);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.EmployeeRole);
    }

    [Fact]
    public void Should_Have_Error_MaximumLength_Name_When_Role_Is_Null()
    {
        // Arrange
        _model.EmployeeRole = EmployeeType.NULL;
        _model.Name = new string('s', 21);
        
        // Act
        var result = _validator.TestValidate(_model);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }
    
    [Fact]
    public void Should_Not_Have_Error_MaximumLength_Name_When_Role_Is_Null()
    {
        // Arrange
        _model.EmployeeRole = EmployeeType.NULL;
        _model.Name = new string('s', 19);
        
        // Act
        var result = _validator.TestValidate(_model);
        
        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Name);
    }
    
    [Theory]
    [InlineData(501)]
    [InlineData(600)]
    public void Should_Have_Error_LessThanOrEqualTo_MaxSalary_When_Role_Is_Null(decimal value)
    {
        // Arrange
        _model.EmployeeRole = EmployeeType.NULL;
        _model.MaxSalary = value;
        
        // Act
        var result = _validator.TestValidate(_model);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.MaxSalary);
    }
    
    [Theory]
    [InlineData(499)]
    [InlineData(500)]
    public void Should_Not_Have_Error_LessThanOrEqualTo_MaxSalary_When_Role_Is_Null(decimal value)
    {
        // Arrange
        _model.EmployeeRole = EmployeeType.NULL;
        _model.MaxSalary = value;
        
        // Act
        var result = _validator.TestValidate(_model);
        
        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.MaxSalary);
    }

    [Fact]
    public void Shoul_Have_Error_NotNull_When_Address_Is_Null()
    {
        // Arrange
        _model.Addresses = null;

        // Act
        var result = _validator.TestValidate(_model);

        // Assert
        // If the child property is null, then the child validator will not be executed.
        result.ShouldHaveValidationErrorFor(x => x.Addresses);
    }
    
    [Fact]
    public void Should_Have_Error_NotNull_When_Address_Is_Not_Null()
    {
        // Arrange
        _model.Addresses = new[] { new Address() };

        // Act
        var result = _validator.TestValidate(_model);

        // Assert
        // If the child property is not null, then the child validator will be executed.
        // Result on this validation
        // Properties with Validation Errors:
        // [0]: Name
        // [1]: EmployeeRole
        // [2]: Addresses[0].Street
        result.ShouldHaveValidationErrorFor(x => x.Addresses);
    }

    [Fact]
    public void Should_Have_Error_MaxLenght_Address_Street_When_Role_Is_Admin()
    {
        // Arrange
        _model.EmployeeRole = EmployeeType.ADMIN;
        _model.Addresses = new Address[]
        {
            new Address()
            {
                Street = new string('s',101)
            }
        };
        
        // Act
        var result = _validator.TestValidate(_model);
        
        // Assert
        // Check with the team, becasue we have original rule and new Child rule added with Role conditional
        // 'Name' no debería estar vacío.
        // 'Street' debe ser menor o igual que 50 caracteres. Ingresó 101 caracter(es).
        // 'Street' debe ser menor o igual que 100 caracteres. Ingresó 101 caracter(es).
        result.ShouldHaveValidationErrorFor(x => x.Addresses);
    }
    

    public static IEnumerable<object[]> EnumValues()
    {
        foreach (var number in Enum.GetValues(typeof(EmployeeType)))
        {
            yield return new object[] { number };
        }
    }
}