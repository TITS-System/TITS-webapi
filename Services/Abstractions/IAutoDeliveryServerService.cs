using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IAutoDeliveryServerService
    {
        Task SetAutoDeliveryMode(long restaurantId, bool mode);

        Task<bool> GetMode(long restaurantId);
    }
}