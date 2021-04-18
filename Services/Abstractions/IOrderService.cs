using System.Threading.Tasks;
using Models.Dtos;
using Models.DTOs.Misc;

namespace Services.Abstractions
{
    public interface IOrderService
    {
        Task<CreatedDto> Create(CreateOrderDto createOrderDto);
    }
}