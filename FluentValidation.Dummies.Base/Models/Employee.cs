using FluentValidation.Dummies.Base.Enums;

namespace FluentValidation.Dummies.Base.Models;

public class Employee
{
    public string Name { get; set; }
    public EmployeeType EmployeeRole { get; set; }
    public Decimal MaxSalary { get; set; }
    public Address[] Addresses { get; set; }
    public Department Department { get; set; }
}