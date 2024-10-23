using Microsoft.EntityFrameworkCore;
using TestTask.DataAccess;
using TestTask.Domain.Models;

namespace TestTask.Domain.Services;

public class DeliveryOrderQuery
{
    public string Region { get; set; }
    public DateTime? FirstOrderDate { get; set; }

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Region))
            throw new ArgumentException("Parameter [cityDistrict] is not specified");

        if (FirstOrderDate == null)
            throw new ArgumentException("Parameter [firstDeliveryDateTime] is not specified");
    }
}

public interface IDeliveryOrderService
{
    Task<IEnumerable<DeliveryOrder>> GetOrdersList();
    Task<IEnumerable<DeliveryOrder>> FetchOrders(DeliveryOrderQuery query);
    Task<IEnumerable<FetchedDeliveryOrder>> GetFetchedOrdersList();
}

public class DeliveryOrderService(AppDbContext dbContext) : IDeliveryOrderService
{
    public async Task<IEnumerable<DeliveryOrder>> GetOrdersList()
    {
        return await dbContext.DeliveryOrders.AsNoTracking().ToArrayAsync();
    }

    public async Task<IEnumerable<DeliveryOrder>> FetchOrders(DeliveryOrderQuery query)
    {
        query = query ?? new DeliveryOrderQuery();

        query.Validate();

        var orders = dbContext.DeliveryOrders.AsNoTracking();

        var normalizedRegion = char.ToUpper(query.Region[0]) + query.Region[1..].ToLower();

        orders = orders.Where(o => normalizedRegion.Equals(o.CityRegion));

        orders = orders.Where(o => o.DeliveryDate >= query.FirstOrderDate && o.DeliveryDate <= query.FirstOrderDate.Value.AddMinutes(30));

        var result = await orders.ToArrayAsync();

        await dbContext.FetchedDeliveryOrders.AddRangeAsync(result.Select(order => new FetchedDeliveryOrder
        {
            CityRegion = order.CityRegion,
            DeliveryDate = order.DeliveryDate,
            Weight = order.Weight,
            RegionQuery = query.Region,
            FirstOrderDateQuery = query.FirstOrderDate.Value,
            QueryDate = DateTime.Now
        }).ToArray());

        await dbContext.SaveChangesAsync();

        return result;
    }

    public async Task<IEnumerable<FetchedDeliveryOrder>> GetFetchedOrdersList()
    {
        return await dbContext.FetchedDeliveryOrders.AsNoTracking().ToArrayAsync();
    }
}
