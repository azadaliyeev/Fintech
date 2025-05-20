using System.Net;
using AutoMapper;
using Fintech.Application.Directors;
using Fintech.Domain.Entities;
using Fintech.Domain.Models.User;
using Fintech.Domain.Models.User.FilteredRequest;
using Fintech.Domain.Models.User.Update;
using Fintech.Domain.Models.User.Verification;
using Fintech.Domain.Services.Accounts;
using Fintech.Domain.Services.Email;
using Fintech.Domain.Services.Users;
using Fintech.Domain.Services.VerificationToken;
using Fintech.Domain.UnitOfWork;
using Fintech.Shared.Enums;
using Fintech.Shared.Extension;
using Fintech.Shared.ServiceResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Fintech.Application.Services.UserService;

public class UserService(
    IUnitOfWork unitOfWork,
    IAccountService accountService,
    IVerificationTokenService verificationTokenService,
    IEmailService emailService,
    UserDirector userDirector,
    IMapper mapper) : IUserService
{
    public async Task CheckUserVerificationAsync()
    {
        var users = await unitOfWork.UserRepository.GetAll()
            .Include(x => x.VerificationToken)
            .Where(x => x.VerificationToken != null).ToListAsync();

        if (!users.Any())
            return;

        var nonVerifiedUsers = users.Where(x => x.VerificationToken!.ExpireDate < DateTime.UtcNow).ToList();

        if (!nonVerifiedUsers.Any())
            return;


        await unitOfWork.UserRepository.DeleteDataFromNonVerifiedUsers(nonVerifiedUsers);
        await verificationTokenService.DeleteTokensAsync(nonVerifiedUsers);
    }

    public async Task<ServiceResult> DeleteUserAsync(string userId)
    {
        if (string.IsNullOrEmpty(userId))
            return ServiceResult.Fail(ErrorMessages.Null.GetMessage());

        var user = await unitOfWork.UserRepository.GetByIdAsync(userId);

        if (user is { Id: "" })
            return ServiceResult.Fail(ErrorMessages.UserNotExistsId.GetMessage());

        if (user.Status == Status.Deleted.Get())
            return ServiceResult.Fail(ErrorMessages.AlreadyDeleted.GetMessage());

        user.Status = Status.Deleted.Get();

        await unitOfWork.UserRepository.DeleteUserAsync(userId);

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    public async Task<ServiceResult> BlockUserAsync(string userId)
    {
        if (string.IsNullOrEmpty(userId))
            return ServiceResult.Fail(ErrorMessages.Null.GetMessage());

        var user = await unitOfWork.UserRepository.GetByIdAsync(userId);

        if (user is { Id: "" })
            return ServiceResult.Fail(ErrorMessages.UserNotExistsId.GetMessage());

        if (user.Status == Status.Blocked.Get())
            return ServiceResult.Fail(ErrorMessages.AlreadyBlocked.GetMessage());


        user.Status = Status.Blocked.Get();

        unitOfWork.UserRepository.Update(user);
        await unitOfWork.CardRepository.BlockCardsByUserIdAsync(userId, Status.Blocked.Get());
        await unitOfWork.AccountRepository.BlockAccountsByUserIdAsync(userId, Status.Blocked.Get());
        await unitOfWork.CommitAsync();

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    public async Task<ServiceResult<Dictionary<string, object>>> GetUserByFilterAsync(UserFilteredRequest request)
    {
        var user = await unitOfWork.UserRepository.GetByIdAsync(request.UserId);

        if (user is { Id: "" })
            return ServiceResult<Dictionary<string, object>>.Fail(ErrorMessages.UserNotExistsId.GetMessage());

        userDirector.Build(request.Fields ?? new List<string?>(), user);

        var result = userDirector.GetResult();

        return ServiceResult<Dictionary<string, object>>.Success(result);
    }

    public async Task<ServiceResult> VerifyPasswordAsync(PasswordVerificationRequest request)
    {
        var result = await verificationTokenService.VerifyUserTokenAsync(request.Token);

        if (!result.IsValid)
            return ServiceResult.Fail(result.Message!);

        var user = await unitOfWork.UserRepository.GetByIdAsync(result.UserId!);
        user!.Password = request.Password;
        var passwordHasher = new PasswordHasher<User>();
        user.Password = passwordHasher.HashPassword(user, user.Password);

        unitOfWork.UserRepository.Update(user);
        await unitOfWork.CommitAsync();

        return ServiceResult.Success(SuccesMessages.PasswordUpdatedSuccess.Get());
    }

    public async Task<ServiceResult> ForgotPasswordAsync(string userId)
    {
        var user = await unitOfWork.UserRepository.GetByIdAsync(userId);

        if (user is { Id: "" })
            return ServiceResult.Fail(ErrorMessages.UserNotExistsId.GetMessage());

        if (!user.IsVerified)
            return ServiceResult.Fail(ErrorMessages.UserNotVerified.GetMessage());

        var verificationToken = await verificationTokenService.CreateTokenAsync(userId);

        await emailService.SendResetPasswordEmailAsync(user.Email, $"{user.FirstName},{user.LastName}",
            verificationToken.Token);

        return ServiceResult.Success(SuccesMessages.ResetPasswordEmailSent.Get());
    }

    public async Task<ServiceResult> UpdateAsync(UpdateUserRequest request)
    {
        var existingUser = await unitOfWork.UserRepository.GetByIdAsync(request.UserId);

        if (existingUser is null)
            return ServiceResult.Fail(ErrorMessages.UserNotExistsId.GetMessage());

        mapper.Map(request, existingUser);

        unitOfWork.UserRepository.Update(existingUser);
        await unitOfWork.CommitAsync();


        return ServiceResult.Success(SuccesMessages.UpdatedSuccessfully.Get(), HttpStatusCode.NoContent);
    }

    public async Task<ServiceResult> EmailVerificationRequestAsync(UserVerificationRequest request)
    {
        var user = await unitOfWork.UserRepository.GetAll()
            .Include(x => x.VerificationToken).FirstOrDefaultAsync(x => x.Id == request.UserId) ?? new User();

        if (user is { Id: "" })
            return ServiceResult.Fail(ErrorMessages.UserNotExistsId.GetMessage());

        if (user.IsVerified)
            return ServiceResult.Fail(ErrorMessages.UserAlreadyVerified.GetMessage());

        if (user.VerificationToken is not null && user.VerificationToken.ExpireDate > DateTime.UtcNow)
            return ServiceResult.Fail(ErrorMessages.VerificationAlreadySent.GetMessage());

        var verificationToken = await verificationTokenService.CreateTokenAsync(request.UserId);

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Email = request.Email;

        unitOfWork.UserRepository.Update(user);
        await unitOfWork.CommitAsync();

        await emailService.SendVerificationEmailAsync(request.Email, $"{request.FirstName},{request.LastName}",
            verificationToken.Token);

        return ServiceResult.Success("Verification email sent successfully");
    }

    public async Task<ServiceResult> ConfirmEmailVerificationAsync(string token)
    {
        var result = await verificationTokenService.VerifyUserTokenAsync(token);

        if (!result.IsValid)
            return ServiceResult.Fail(result.Message!);


        var user = await unitOfWork.UserRepository.GetByIdAsync(result.UserId!);

        user!.IsVerified = true;

        unitOfWork.UserRepository.Update(user);
        await unitOfWork.CommitAsync();

        return ServiceResult.Success(SuccesMessages.UserVerifiedSuccess.Get());
    }

    public async Task CreateInAsync(User user)
    {
        user.Id = Guid.NewGuid().ToString();
        var passwordHasher = new PasswordHasher<User>();
        user.Password = passwordHasher.HashPassword(user, user.Password);
        await unitOfWork.UserRepository.AddAsync(user);
        await accountService.CreateInAsync(user.Id);
    }

    public async Task<ServiceResult<UserDto>> GetByIdAsync(string id)
    {
        var user = await unitOfWork.UserRepository.GetByIdAsync(id);

        if (user is { Id: "" })
            return ServiceResult<UserDto>.Fail(ErrorMessages.UserNotExistsId.GetMessage());

        return ServiceResult<UserDto>.Success(mapper.Map<UserDto>(user));
    }

    public async Task<bool> IsUserExistsAsync(string id) => await unitOfWork.UserRepository.ExistByIdAsync(id);

    public async Task<ServiceResult<List<UserDto>>> GetByPagination(int pageNumber, int pageSize)
    {
        var skip = (pageNumber - 1) * pageSize;

        var users = await unitOfWork.UserRepository.GetAll().Skip(skip)
            .Take(pageSize).ToListAsync();

        if (users.Count == 0)
            return ServiceResult<List<UserDto>>.Fail(ErrorMessages.UserNotExistsId.GetMessage());

        var userAsDto = mapper.Map<List<UserDto>>(users);

        return ServiceResult<List<UserDto>>.Success(userAsDto);
    }
}