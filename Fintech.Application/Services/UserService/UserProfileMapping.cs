using AutoMapper;
using Fintech.Domain.Entities;
using Fintech.Domain.Models.User;
using Fintech.Domain.Models.User.Update;

namespace Fintech.Application.Services.UserService;

public class UserProfileMapping : Profile
{
    public UserProfileMapping()
    {
        CreateMap<User, UserDto>().ReverseMap();

        CreateMap<UpdateUserRequest, User>()
            .ForAllMembers(opts => 
                opts.Condition((src, dest, srcMember) => srcMember != null));
        
        
    }
}