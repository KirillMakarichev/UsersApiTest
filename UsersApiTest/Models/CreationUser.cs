using System.ComponentModel.DataAnnotations;

namespace UsersApiTest.Models;

public class CreationUser
{
    public string Name { get; set; }
    
    [Phone]
    public string Phone { get; set; }
    
    [EmailAddress]
    public string Email { get; set; }
}