using Fintech.Domain.Entities;
using Fintech.Domain.Models.User;
using Fintech.Domain.Models.User.FilteredRequest;
using Fintech.Domain.Models.User.Update;
using Fintech.Domain.Models.User.Verification;
using Fintech.Shared.ServiceResults;

namespace Fintech.Domain.Services.Users;

public interface IUserService
{
    Task CreateInAsync(User user);
    Task<ServiceResult<UserDto?>> GetByIdAsync(string id);
    Task<bool> IsUserExistsAsync(string id);
    Task<ServiceResult> BlockUserAsync(string userId);
    Task<ServiceResult> VerifyPasswordAsync(PasswordVerificationRequest request);

    Task<ServiceResult> EmailVerificationRequestAsync(UserVerificationRequest request);
    Task<ServiceResult> ConfirmEmailVerificationAsync(string token);
    Task<ServiceResult> ForgotPasswordAsync(string userId);
    Task<ServiceResult<List<UserDto>>> GetByPagination(int pageNumber, int pageSize);
    Task<ServiceResult> UpdateAsync(UpdateUserRequest request);

    Task<ServiceResult<Dictionary<string, object>>> GetUserByFilterAsync(UserFilteredRequest request);

    Task<ServiceResult> DeleteUserAsync(string userId);
}