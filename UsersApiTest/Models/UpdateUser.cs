using System.ComponentModel.DataAnnotations;

namespace UsersApiTest.Models;

public class UpdateUser
{
    public int Id { get; set; }
    public string? Name { get; set; }
    
    [Phone]
    public string? Phone { get; set; }
    
    [EmailAddress]
    public string? Email { get; set; }
}