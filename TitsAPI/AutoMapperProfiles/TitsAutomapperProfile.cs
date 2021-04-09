using AutoMapper;
using Models.Db;
using Models.DTOs.General;
using Models.DTOs.Requests;
using Models.DTOs.Responses;

namespace TitsAPI.AutoMapperProfiles
{
    // --------------------------------------------------------- //
    // EVEN IF YOUR IDE SAYS THIS CODE IS UNUSED, DONT DELETE IT //
    // --------------------------------------------------------- //

    public class TitsAutomapperProfile : Profile
    {
        public TitsAutomapperProfile()
        {
            // ReverseMap() нужен для обратной конвертации любого мапа

            // Базовые мапы для DTO
            // -------------------------------------------------
            CreateMap<IngredientDto, IngredientTemplate>().ReverseMap();
            CreateMap<IngredientDto, OrderIngredient>().ReverseMap();

            CreateMap<ProductDto, OrderProduct>().ReverseMap();
            CreateMap<ProductDto, ProductTemplate>().ReverseMap();

            CreateMap<ProductPackDto, OrderProductPack>().ReverseMap();
            CreateMap<ProductPackDto, ProductPackTemplate>().ReverseMap();

            CreateMap<OrderDto, Order>().ReverseMap();
            // -------------------------------------------------

            // Create DTOs
            // -------------------------------------------------
            CreateMap<CreateIngredientDto, IngredientTemplate>().ReverseMap();
            CreateMap<CreateIngredientDto, OrderIngredient>().ReverseMap();

            CreateMap<CreateProductDto, ProductTemplate>().ReverseMap();
            CreateMap<CreateProductDto, OrderProduct>().ReverseMap();

            CreateMap<CreateProductPackDto, ProductPackTemplate>().ReverseMap();
            CreateMap<CreateProductPackDto, OrderProductPack>().ReverseMap();

            CreateMap<CreateOrderDto, Order>().ReverseMap();
            // -------------------------------------------------

            // Create Template-To-Order Mappings
            // -------------------------------------------------
            CreateMap<IngredientTemplate, OrderIngredient>()
                .ForMember(oi=>oi.Id, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(it=>it.Id, opt => opt.Ignore());
            CreateMap<ProductTemplate, OrderProduct>()
                .ForMember(op=>op.Id, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(pt=>pt.Id, opt => opt.Ignore());
            CreateMap<ProductPackTemplate, OrderProductPack>()
                .ForMember(opp=>opp.Id, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(ppt=>ppt.Id, opt => opt.Ignore());
            // -------------------------------------------------

            CreateMap<ProductCategoryDto, ProductCategory>().ReverseMap();

            CreateMap<AccountRoleDto, AccountRole>().ReverseMap();
        }
    }
}