namespace FitphoneBackend.Business.DTOs.Profiles
{
    using AutoMapper;
    using FitphoneBackend.Business.DTOs.PhoneUsage;
    using FitPhoneBackend.Business.Entities;
   
    using static System.Runtime.InteropServices.JavaScript.JSType;

    public class PhoneUsageProfile : Profile
    {
        public PhoneUsageProfile()
        {
            CreateMap<PhoneUsage, PhoneUsageDto>();
            CreateMap<PhoneUsageCreateDto, PhoneUsage>()
                .ConstructUsing(dto =>
                new PhoneUsage(Guid.NewGuid(), dto.UserId, dto.UnlockCount, dto.ScreenTimeMinutes));
        }
    }

}
