using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IAutoDeliveryServerService
    {
        Task SetAutoDeliveryMode(bool mode);

        Task<bool> GetMode();
    }
}