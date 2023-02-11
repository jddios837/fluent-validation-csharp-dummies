# Fluent Validation For Dummies
## Models Definitions
### Employee
    - Name : String | Validations (MaxLenght(50),NotEmpty)
    - EmployeeRole [Enum] | Validations (IsEnum)
    - MaxSalary : decimal 
        -Validations (Depend on EmployeeRole)
            1. EmployeeRole.Normal MaxSalary Max(1000)
            2. EmployeeRole.IT MaxSalary Max(2000)
            3. EmployeeRole.ADMIN MaxSalary Max(3000)
            4. EmployeeRole.NULL MaxSalary Max(500)
    - Address : Array[Address] | Validation 
    - Department : Department[Object]

### EmployeeRole Enum
    - NULL
    - IT
    - ADMIN
    - NORMAL

### Address
    - Street : String | Validation (MaxLenght(50),NotEmpty)
        - When Employee.EmployeeRole is ADMIN MaxLenght(100)?
    - City : String
    - PostalCode : int

### Department
    - Name : string
    - Description : string
