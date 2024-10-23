using Microsoft.AspNetCore.Mvc;
using TestTask.Domain.Models;
using TestTask.Domain.Services;

namespace TestTask.Controllers;

[ApiController]
[Route("delivery-order")]
public class DeliveryOrderController(IDeliveryOrderService service, ILogger<DeliveryOrderController> logger) : ControllerBase
{
    [HttpGet("list")]
    public async Task<IEnumerable<DeliveryOrder>> GetOrdersList()
    {
        return await service.GetOrdersList();
    }

    [HttpGet("filter")]
    public async Task<IEnumerable<DeliveryOrder>> FetchOrders(string cityDistrict, DateTime firstDeliveryDateTime)
    {
        return await service.FetchOrders(new DeliveryOrderQuery
        {
            Region = cityDistrict,
            FirstOrderDate = firstDeliveryDateTime
        });
    }

    [HttpGet("fetched")]
    public async Task<IEnumerable<FetchedDeliveryOrder>> GetFetchedOrdersList()
    {
        return await service.GetFetchedOrdersList();
    }
}
