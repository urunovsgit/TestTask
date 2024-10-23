using Microsoft.AspNetCore.Mvc;
using TestTask.Domain.Models;
using TestTask.Domain.Services;

namespace TestTask.Controllers;

[ApiController]
[Route("delivery-order")]
public class DeliveryOrderController(IDeliveryOrderService service, ILogger<DeliveryOrderController> logger) : ControllerBase
{
    [HttpGet("list")]
    public async Task<IEnumerable<DeliveryOrder>> Get(string cityDistrict, DateTime? firstDeliveryDateTime)
    {
        return await service.GetOrders(new DeliveryOrderQuery
        {
            Regions = string.IsNullOrWhiteSpace(cityDistrict) ? null : [cityDistrict],
            FirstOrderDate = firstDeliveryDateTime
        });
    }
}
