using Microsoft.EntityFrameworkCore;
using TestTask.Domain.Models;

namespace TestTask.DataAccess;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<DeliveryOrder> DeliveryOrders { get; set; }
    public DbSet<FetchedDeliveryOrder> FetchedDeliveryOrders { get; set; }
}
