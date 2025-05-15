using System.Net;
using AutoMapper;
using Fintech.Domain.Entities;
using Fintech.Domain.Models.Authentication.Login;
using Fintech.Domain.Models.Authentication.SignUp;
using Fintech.Domain.Services.Authentication;
using Fintech.Domain.Services.Users;
using Fintech.Domain.UnitOfWork;
using Fintech.Shared.Enums;
using Fintech.Shared.Extension;
using Fintech.Shared.ServiceResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Fintech.Application.Services.Authentication;

public class Authentication(
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IUserService userService,
    ILogger<Authentication> logger) : IAuthentication
{
    private readonly ILogger<Authentication> _logger = logger;

    public async Task<ServiceResult<SignUpResponse>> SignUpAsync(SignUpRequest request)
    {
        var checkPhone = await unitOfWork.UserRepository.ExistByPhoneNumberAsync(request.PhoneNumber);
        var checkEmail = await unitOfWork.UserRepository.ExistByEmailAsync(request.Email);

        if (checkPhone)
            return ServiceResult<SignUpResponse>.Fail(ErrorMessages.PhoneNumberExists.GetMessage());

        if (checkEmail)
            return ServiceResult<SignUpResponse>.Fail(ErrorMessages.EmailExists.GetMessage());

        var user = mapper.Map<User>(request);


        await userService.CreateInAsync(user);
        await unitOfWork.CommitAsync();

        return ServiceResult<SignUpResponse>.SuccessAsCreated(new SignUpResponse(user.Id,"Signup process is Successfully"),
            $"/api/users/{user.Id}");
    }


    public async Task<ServiceResult<LoginResponse>> LoginAsync(LoginRequest request)
    {
        var user = await unitOfWork.UserRepository.GetByEmailAsync(request.Email);

        if (user is null)
        {
            return ServiceResult<LoginResponse>.Fail(ErrorMessages.EmailNotExists.GetMessage(),
                HttpStatusCode.NotFound);
        }

        var hasher = new PasswordHasher<User>();

        var verification = hasher.VerifyHashedPassword(user, user.Password, request.Password);

        if (verification == PasswordVerificationResult.Failed)
        {
            return ServiceResult<LoginResponse>.Fail(ErrorMessages.InvalidPassword.GetMessage(),
                HttpStatusCode.Unauthorized);
        }

        var loginResponse = mapper.Map<LoginResponse>(user);

        return ServiceResult<LoginResponse>.Success(loginResponse);
    }
}