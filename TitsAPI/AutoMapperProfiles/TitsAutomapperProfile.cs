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
            CreateMap<IngredientDto, MenuIngredient>().ReverseMap();
            CreateMap<IngredientDto, OrderIngredient>().ReverseMap();

            CreateMap<ProductDto, OrderProduct>().ReverseMap();
            CreateMap<ProductDto, MenuProduct>().ReverseMap();

            CreateMap<ProductPackDto, OrderProductPack>().ReverseMap();
            CreateMap<ProductPackDto, MenuProductPack>().ReverseMap();

            CreateMap<OrderDto, Order>().ReverseMap();
            // -------------------------------------------------

            // Create DTOs
            // -------------------------------------------------
            CreateMap<CreateIngredientDto, MenuIngredient>().ReverseMap();
            CreateMap<CreateIngredientDto, OrderIngredient>().ReverseMap();

            CreateMap<CreateProductDto, MenuProduct>().ReverseMap();
            CreateMap<CreateProductDto, OrderProduct>().ReverseMap();

            CreateMap<CreateProductPackDto, MenuProductPack>().ReverseMap();
            CreateMap<CreateProductPackDto, OrderProductPack>().ReverseMap();

            CreateMap<CreateOrderDto, Order>().ReverseMap();
            // -------------------------------------------------

            // Create Template-To-Order Mappings
            // -------------------------------------------------
            CreateMap<MenuIngredient, OrderIngredient>()
                .ForMember(oi=>oi.Id, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(it=>it.Id, opt => opt.Ignore());
            CreateMap<MenuProduct, OrderProduct>()
                .ForMember(op=>op.Id, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(pt=>pt.Id, opt => opt.Ignore());
            CreateMap<MenuProductPack, OrderProductPack>()
                .ForMember(opp=>opp.Id, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(ppt=>ppt.Id, opt => opt.Ignore());
            // -------------------------------------------------

            CreateMap<ProductCategoryDto, ProductCategory>().ReverseMap();

            CreateMap<AccountRoleDto, AccountRole>().ReverseMap();
        }
    }
}