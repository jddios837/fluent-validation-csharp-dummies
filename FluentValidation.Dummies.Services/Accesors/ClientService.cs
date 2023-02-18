namespace FluentValidation.Dummies.Services.Accesors;

public class ClientService
{
    public string? Name { get; set; }
    private int? Total { get; set; }
    
    // Only could be access from members of this class
    // and child classes that inhered from this class
    protected string? Description { get; set; }
    
    // Only could be access from any class of the same 
    // project but is not accessible from others projects
    internal string? Phone { get; set; }
    
    public IList<int> GetAllUsersId()
    {
        List<int> list = new List<int>(){2,3,4};
        return list;
    }
    
}