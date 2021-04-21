using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IAutoDeliveryServerService
    {
        Task SetAutoDeliveryMode(bool mode);
    }
}