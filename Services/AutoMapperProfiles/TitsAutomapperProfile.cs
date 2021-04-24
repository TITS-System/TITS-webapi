using System;
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

            CreateMap<DateTime, string>().ConstructUsing(src => src.ToString("yyyy-MM-dd dddd HH\\:mm\\:ss"));
            CreateMap<DateTime?, string>().ConstructUsing(src => src != null ? src.Value.ToString("yyyy-MM-dd dddd HH\\:mm\\:ss") : "");

            CreateMap<WorkerRoleDto, WorkerRole>().ReverseMap();
            CreateMap<CreateCourierAccountDto, CourierAccount>().ReverseMap();
            CreateMap<CreateManagerAccountDto, ManagerAccount>().ReverseMap();

            CreateMap<CreateOrderDto, Order>()
                .ForMember(
                    dto => dto.DestinationLatLng,
                    cfg => cfg.Ignore())
                .ReverseMap();

            CreateMap<Delivery, DeliveryDto>().ForMember(dto => dto.CourierUsername, cfg => cfg.MapFrom(o => o.CourierAccount.Username)).ReverseMap();

            CreateMap<ManagerFullInfoDto, ManagerAccount>().ReverseMap();
            CreateMap<CourierFullInfoDto, CourierAccount>().ReverseMap();

            CreateMap<UnservedOrderDto, Order>().ReverseMap();
            CreateMap<OrderDto, Order>().ReverseMap();

            CreateMap<CourierAccount, CourierAccountDto>().ForMember(
                dto => dto.IsOnWork,
                cfg => cfg.MapFrom(a => a.LastCourierSessionId)
            ).ReverseMap();
            //.ForMember(dto => dto.IsOnWork, cfg => cfg.MapFrom(account => account.LastCourierSessionId != null)).ReverseMap();

            CreateMap<CourierMessageDto, CourierMessage>().ReverseMap();

            CreateMap<SosDto, SosRequest>().ReverseMap();

            CreateMap<RestaurantDto, Restaurant>().ReverseMap();

            CreateMap<LatLng, LatLngDto>().ReverseMap();
        }
    }
}