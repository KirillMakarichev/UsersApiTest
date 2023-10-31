using Microsoft.AspNetCore.Mvc;
using UsersApiTest.Models;
using UsersApiTest.Repository.Interfaces;

namespace UsersApiTest.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers(
        [FromQuery] List<int> ids,
        [FromQuery] List<string> names,
        [FromQuery] List<string> phones,
        [FromQuery] List<string> emails)
    {
        var users = await _userRepository.GetUsersAsync(ids, names, phones, emails);

        if (!users.Any()) return NotFound();

        return Ok(users);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(CreationUser creation)
    {
        if (creation == null) return BadRequest();
        
        var createdUser = await _userRepository.CreateUserAsync(new User()
        {
            Name = creation.Name,
            Phone = creation.Phone,
            Email = creation.Email
        });

        if (createdUser.Id == 0) return BadRequest();
        
        return Ok(createdUser);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser(UpdateUser update)
    {
        if (update is not { Id: > 0 }) return BadRequest();
        
        var updatedUser = await _userRepository.UpdateUserAsync(new User()
        {
            Id = update.Id,
            Phone = update.Phone,
            Email = update.Email,
            Name = update.Name
        });

        if (updatedUser.Id == 0) return NotFound();
        
        return Ok(updatedUser);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUser(int id)
    {
        if (id <= 0) return BadRequest();

        var deleted = await _userRepository.DeleteUserAsync(id);
        
        return deleted ? Ok() : NotFound();
    }
}