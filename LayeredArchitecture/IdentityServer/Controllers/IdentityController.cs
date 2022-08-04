using IdentityServiceClient.Models;
using IdentityServiceClient.Services;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers;

[ApiController]
public class IdentityController: ControllerBase
{
    private readonly IRoleService _roleService;
    private readonly IUserService _userService;

    public IdentityController(IUserService userService, IRoleService roleService)
    {
        _userService = userService;
        _roleService = roleService;
    }

    [HttpPost("users")]
    public async Task<ActionResult> AddUser(User user)
    {
        await _userService.Add(user);

        return Ok();
    }

    [HttpPost("roles")]
    public async Task<ActionResult> AddRole(Role role)
    {
        await _roleService.Add(role);

        return Ok();
    }

    [HttpGet("users")]
    public async Task<ActionResult<List<User>>> GetUsers()
    {
        var result = await _userService.GetAll();

        return Ok(result);
    }

    [HttpGet("roles")]
    public async Task<ActionResult<List<Role>>> GetRoles()
    {
        var result = await _roleService.GetAll();

        return Ok(result);
    }
}