using Services.Abstractions;
using TitsAPI.Controllers;

namespace TitsAPI.Areas.API
{
    public class DeliveryController : TitsController
    {
        public DeliveryController(ITokenSessionService tokenSessionService) : base(tokenSessionService)
        {
        }
    }
}