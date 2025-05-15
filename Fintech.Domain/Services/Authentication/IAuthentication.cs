using Fintech.Shared;
using Fintech.Domain.Entities;
using Fintech.Domain.Models.Authentication.Login;
using Fintech.Domain.Models.Authentication.SignUp;
using Fintech.Shared.ServiceResults;

namespace Fintech.Domain.Services.Authentication;

public interface IAuthentication
{
    Task<ServiceResult<SignUpResponse>> SignUpAsync(SignUpRequest request);

    Task<ServiceResult<LoginResponse>> LoginAsync(LoginRequest request);
}