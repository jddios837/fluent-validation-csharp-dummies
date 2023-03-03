using FluentValidation.Dummies.Base.Models;
using Microsoft.AspNetCore.Mvc;

namespace FluentValidation.Dummies.API.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly ILogger<EmployeeController> _logger;

    public EmployeeController(ILogger<EmployeeController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public bool Get([FromBody] Employee employee)
    {
        _logger.Log(LogLevel.Information,"Information Test {@TEST}", employee);
        _logger.Log(LogLevel.Error,"Error Test {@TEST}", employee);
        _logger.Log(LogLevel.Critical,"Critical Test {@TEST}", employee);
        
        return true;
    }
}