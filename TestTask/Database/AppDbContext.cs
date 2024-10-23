using Microsoft.EntityFrameworkCore;
using TestTask.Domain.Models;

namespace TestTask.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<DeliveryOrder> DeliveryOrders { get; set; }
}
