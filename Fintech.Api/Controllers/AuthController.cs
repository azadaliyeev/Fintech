using Fintech.Domain.Models.Authentication.SignUp;
using Fintech.Domain.Services.Authentication;
using Fintech.Domain.Services.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using LoginRequest = Fintech.Domain.Models.Authentication.Login.LoginRequest;

namespace Fintech.Api.Controllers;

public class AuthController(IAuthentication authService) : CustomBaseController
{
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest request)
        => CreateActionResult(await authService.LoginAsync(request));
    
    [HttpPost("register")]
    public async Task<IActionResult> SignUp (SignUpRequest request)
        => CreateActionResult(await authService.SignUpAsync(request));
}