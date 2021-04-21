using System.Threading.Tasks;
using Models.Dtos;
using Models.DTOs.Misc;

namespace Services.Abstractions
{
    public interface IOrderService
    {
        Task<CreatedDto> Create(CreateOrderDto createOrderDto);

        Task<GetUnservedOrdersResultDto> GetUnserved(long restaurantId);

        Task<OrderDto> GetInfo(long id);

        Task<OrdersDto> GetAllByRestaurant(long restaurantId);
    }
}