using AutoMapper;
using Models.Db;
using Models.Db.Account;
using Models.Dtos;
using Models.DTOs.Misc;
using Models.DTOs.Responses;
using Models.DTOs.WorkerAccountDtos;

namespace Services.AutoMapperProfiles
{
    // --------------------------------------------------------- //
    // EVEN IF YOUR IDE SAYS THIS CODE IS UNUSED, DONT DELETE IT //
    // --------------------------------------------------------- //

    public class TitsAutomapperProfile : Profile
    {
        public TitsAutomapperProfile()
        {
            // ReverseMap() нужен для обратной конвертации любого мапа
            
            CreateMap<long?, bool>().ConstructUsing(src => src != null);
            
            CreateMap<WorkerRoleDto, WorkerRole>().ReverseMap();
            CreateMap<CreateCourierAccountDto, CourierAccount>().ReverseMap();
            CreateMap<CreateManagerAccountDto, ManagerAccount>().ReverseMap();

            CreateMap<CreateOrderDto, Order>()
                .ForMember(
                    dto => dto.DestinationLatLng,
                    cfg => cfg.Ignore())
                .ReverseMap();

            CreateMap<UnservedOrderDto, Order>().ReverseMap();

            CreateMap<CourierAccount, CourierAccountDto>().ReverseMap();
                //.ForMember(dto => dto.IsOnWork, cfg => cfg.MapFrom(account => account.LastCourierSessionId != null)).ReverseMap();

            CreateMap<CourierMessageDto, CourierMessage>().ReverseMap();

            CreateMap<SosDto, SosRequest>().ReverseMap();

            CreateMap<RestaurantDto, Restaurant>().ReverseMap();

            CreateMap<LatLng, LatLngDto>().ReverseMap();
        }
    }
}