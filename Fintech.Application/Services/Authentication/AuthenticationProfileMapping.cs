using AutoMapper;
using Fintech.Domain.Entities;
using Fintech.Domain.Models.Authentication.Login;
using Fintech.Domain.Models.Authentication.SignUp;

namespace Fintech.Application.Services.Authentication;

public class AuthenticationProfileMapping : Profile
{
    public AuthenticationProfileMapping()
    {
        CreateMap<SignUpRequest, User>().ReverseMap();
        CreateMap<SignUpResponse, User>().ReverseMap();
        CreateMap<User, LoginResponse>().ReverseMap();

    }
}