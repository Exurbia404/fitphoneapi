namespace FitphoneBackend.Business.DTOs.Profiles
{
    using AutoMapper;
    using FitPhoneBackend.Business.DTOs.UserDto;
    using FitPhoneBackend.Business.Entities;
   
    using static System.Runtime.InteropServices.JavaScript.JSType;

    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserCreateDto>();
            CreateMap<UserCreateDto, User>();
        }
    }

}
