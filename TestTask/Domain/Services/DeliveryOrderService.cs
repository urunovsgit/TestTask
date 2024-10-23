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
            throw new ArgumentNullException(nameof(Region));

        if (FirstOrderDate == null)
            throw new ArgumentNullException(nameof(FirstOrderDate));
    }
}

public interface IDeliveryOrderService
{
    Task<IEnumerable<DeliveryOrder>> GetOrdersList();
    Task<IEnumerable<DeliveryOrder>> FetchOrders(DeliveryOrderQuery query);
    Task<IEnumerable<FetchedDeliveryOrder>> GetFetchedOrdersList();
}

public class DeliveryOrderService(ILogger logger, AppDbContext dbContext) : IDeliveryOrderService
{
    public async Task<IEnumerable<DeliveryOrder>> GetOrdersList()
    {
        await logger.Log("Requested DeliveryOrders list");
        return await dbContext.DeliveryOrders.AsNoTracking().ToArrayAsync();
    }

    public async Task<IEnumerable<DeliveryOrder>> FetchOrders(DeliveryOrderQuery query)
    {
        await logger.Log($"Requested DeliveryOrders fetch by params [{query.Region}, {query.FirstOrderDate}]");

        query = query ?? new DeliveryOrderQuery();

        try
        {
            query.Validate();
        }
        catch (Exception ex)
        {
            await logger.Log($"Fetch failed. Error: {ex.Message}", LogType.Error);
            throw;
        }

        var orders = dbContext.DeliveryOrders.AsNoTracking();

        var normalizedRegion = char.ToUpper(query.Region[0]) + query.Region[1..].ToLower();

        orders = orders.Where(o => normalizedRegion.Equals(o.CityRegion));

        orders = orders.Where(o => o.DeliveryDate >= query.FirstOrderDate && o.DeliveryDate <= query.FirstOrderDate.Value.AddMinutes(30));

        var result = await orders.ToArrayAsync();

        await logger.Log($"Fetched DeliveryOrders: {result.Length}");

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

        await logger.Log($"Saving fetched DeliveryOrders history");

        return result;
    }

    public async Task<IEnumerable<FetchedDeliveryOrder>> GetFetchedOrdersList()
    {
        await logger.Log("Requested FetchedDeliveryOrders list");
        return await dbContext.FetchedDeliveryOrders.AsNoTracking().ToArrayAsync();
    }
}
