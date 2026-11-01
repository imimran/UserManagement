using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Api.Application.DTOs.Users;
using UserManagement.Api.Application.Interfaces;
using UserManagement.Api.Common.Responses;

namespace UserManagement.Api.Controllers;

[ApiController]
[Route("api/users")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users =
            await _userService.GetAllAsync();

        return Ok(
            ApiResponse<List<UserResponse>>.Ok(
                users,
                "Users retrieved successfully."));
    }


    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        Guid id)
    {
        var user =
            await _userService.GetByIdAsync(id);

        return Ok(
            ApiResponse<UserResponse>.Ok(
                user,
                "User retrieved successfully."));
    }


    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(
        CreateUserRequest request)
    {
        var user =
            await _userService.CreateAsync(request);

        return StatusCode(
            StatusCodes.Status201Created,
            ApiResponse<UserResponse>.Ok(
                user,
                "User created successfully."));
    }


    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(
        Guid id,
        UpdateUserRequest request)
    {
        var user =
            await _userService.UpdateAsync(
                id,
                request);

        return Ok(
            ApiResponse<UserResponse>.Ok(
                user,
                "User updated successfully."));
    }


    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(
        Guid id)
    {
        await _userService.DeleteAsync(id);

        return Ok(
            ApiResponse<object>.Ok(
                null!,
                "User deleted successfully."));
    }
}