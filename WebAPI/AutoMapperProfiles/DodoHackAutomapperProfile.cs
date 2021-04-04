using AutoMapper;
using Models;
using Models.Dtos;
using Models.Dtos.General;
using Models.Dtos.Requests;
using Models.Dtos.Responses;

namespace WebAPI.AutoMapperProfiles
{
    // --------------------------------------------------------- //
    // EVEN IF YOUR IDE SAYS THIS CODE IS UNUSED, DONT DELETE IT //
    // --------------------------------------------------------- //

    public class DodoHackAutomapperProfile : Profile
    {
        public DodoHackAutomapperProfile()
        {
            CreateMap<LatLng, LatLngDto>().ReverseMap();
            CreateMap<Courier, CourierDto>().ReverseMap();
            CreateMap<Courier, CreateCourierDto>().ReverseMap();
            CreateMap<OrderProduct, OrderProductDto>()
                .ForMember(dto => dto.Title, opt => opt.MapFrom(product => product.ProductTemplate.Title)).ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Order, CreateOrderDto>().ReverseMap();
            CreateMap<OrderProductTemplate, OrderProductTemplateDto>().ReverseMap();
        }
    }
}