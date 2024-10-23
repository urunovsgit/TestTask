using Microsoft.EntityFrameworkCore;
using TestTask.Database;
using TestTask.Domain.Models;

namespace TestTask.Domain.Services;

public class DeliveryOrderQuery
{
    public string[] Regions { get; set; }
    public DateTime? FirstOrderDate { get; set; }
}

public interface IDeliveryOrderService
{
    public Task<IEnumerable<DeliveryOrder>> GetOrders(DeliveryOrderQuery query);
}

public class DeliveryOrderService(AppDbContext dbContext) : IDeliveryOrderService
{
    public async Task<IEnumerable<DeliveryOrder>> GetOrders(DeliveryOrderQuery query)
    {
        var orders = dbContext.DeliveryOrders.AsNoTracking();

        if (query.Regions != null)
            orders = orders.Where(o => query.Regions.Contains(o.CityRegion));

        if(query.FirstOrderDate != null)
            orders = orders.Where(o => o.DeliveryDate >= query.FirstOrderDate && o.DeliveryDate <= query.FirstOrderDate.Value.AddMinutes(30));

        return await orders.ToArrayAsync();
    }
}
