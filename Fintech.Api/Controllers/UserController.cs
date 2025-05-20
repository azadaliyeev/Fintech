using Fintech.Domain.Models.User.FilteredRequest;
using Fintech.Domain.Models.User.Update;
using Fintech.Domain.Models.User.Verification;
using Fintech.Domain.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace Fintech.Api.Controllers;

public class UserController(IUserService userService) : CustomBaseController
{
    [HttpGet]
    public async Task<IActionResult> GetUserById(string id) =>
        CreateActionResult(await userService.GetByIdAsync(id));

    [HttpGet("{pageNumber:int}/{pageSize:int}")]
    public async Task<IActionResult> GetUsersByPagination(int pageNumber, int pageSize) =>
        CreateActionResult(await userService.GetByPagination(pageNumber, pageSize));

    [HttpPost("verify-request")]
    public async Task<IActionResult> VerifyUser([FromBody] UserVerificationRequest request) =>
        CreateActionResult(await userService.EmailVerificationRequestAsync(request));


    [HttpGet("verify/{token}")]
    public async Task<IActionResult> VerifyToken(string token) =>
        CreateActionResult(await userService.ConfirmEmailVerificationAsync(token));

    [HttpPut]
    public async Task<IActionResult> Update(UpdateUserRequest request)
        => CreateActionResult(await userService.UpdateAsync(request));

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(string userId) =>
        CreateActionResult(await userService.ForgotPasswordAsync(userId));

    [HttpPost("verify-password")]
    public async Task<IActionResult> VerifyPassword(PasswordVerificationRequest request) =>
        CreateActionResult(await userService.VerifyPasswordAsync(request));

    [HttpGet("filtered")]
    public async Task<IActionResult> GetUserByFilter([FromQuery] UserFilteredRequest request) =>
        CreateActionResult(await userService.GetUserByFilterAsync(request));

    [HttpPut("block/{userId}")]
    public async Task<IActionResult> BlockUser(string userId) =>
        CreateActionResult(await userService.BlockUserAsync(userId));

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUser(string userId) =>
        CreateActionResult(await userService.DeleteUserAsync(userId));
}