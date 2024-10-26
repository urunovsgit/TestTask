using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Data.Common;
using TestTask.DataAccess;
using TestTask.Domain.Services;

namespace TestTask.Test;


[TestFixture]
public class DeliveryOrderServiceTests : IDisposable
{
    private DeliveryOrderService _deliveryOrderService;
    private SqliteConnection _connection;
    private AppDbContext _context;

    [SetUp]
    public async Task Setup()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(_connection)
            .Options;

        _context = new AppDbContext(options);

        var logger = new Logger(_context);

        _deliveryOrderService = new DeliveryOrderService(logger, _context);

        await PopulateDatabase();
    }

    [Test]
    public async Task GetOrdersList_ShouldReturnOrdersList()
    {
        // Act
        var result = await _deliveryOrderService.GetOrdersList();

        // Assert
        Assert.That(result.Count(), Is.EqualTo(37));
    }

    [Test]
    public async Task FetchOrders_WithValidQuery_ShouldReturnOrdersAndSaveFetchedOrders()
    {
        // Arrange
        var query = new DeliveryOrderQuery()
        {
            Region = "south",
            FirstOrderDate = new DateTime(2024, 10, 23, 8, 10, 0)
        };

        // Act
        var result = await _deliveryOrderService.FetchOrders(query);

        // Assert
        Assert.That(result.Count(), Is.EqualTo(2));
    }

    [Test]
    public void FetchOrders_WithInvalidQuery_ShouldThrowException()
    {
        // Arrange
        var query = new DeliveryOrderQuery
        { 
            Region = "", 
            FirstOrderDate = null 
        };

        // Act & Assert
        var ex = Assert.ThrowsAsync<ArgumentNullException>(async () => await _deliveryOrderService.FetchOrders(query));
    }

    [Test]
    public async Task GetFetchedOrdersList_ShouldReturnFetchedOrdersList()
    {
        // Arrange
        var query = new DeliveryOrderQuery()
        {
            Region = "south",
            FirstOrderDate = new DateTime(2024, 10, 23, 8, 10, 0)
        };

        // Act
        await _deliveryOrderService.FetchOrders(query);

        var result = await _deliveryOrderService.GetFetchedOrdersList();

        // Assert
        Assert.That(result.Count(), Is.EqualTo(2));
    }

    private async Task PopulateDatabase()
    {
        await _context.Database.EnsureDeletedAsync();
        await _context.Database.EnsureCreatedAsync();

        await _context.DeliveryOrders.AddRangeAsync(
        [
            new() { Weight = 5.0m, CityRegion = "Central", DeliveryDate = new DateTime(2024, 10, 23, 8, 0, 0) },
            new() { Weight = 5.2m, CityRegion = "Central", DeliveryDate = new DateTime(2024, 10, 23, 8, 40, 0) },
            new() { Weight = 5.3m, CityRegion = "Central", DeliveryDate = new DateTime(2024, 10, 23, 9, 5, 0) },
            new() { Weight = 4.5m, CityRegion = "Central", DeliveryDate = new DateTime(2024, 10, 23, 9, 30, 0) },
            new() { Weight = 5.1m, CityRegion = "Central", DeliveryDate = new DateTime(2024, 10, 23, 9, 55, 0) },
            new() { Weight = 5.4m, CityRegion = "Central", DeliveryDate = new DateTime(2024, 10, 23, 10, 20, 0) },
            new() { Weight = 4.6m, CityRegion = "Central", DeliveryDate = new DateTime(2024, 10, 23, 10, 45, 0) },

            new() { Weight = 3.2m, CityRegion = "North", DeliveryDate = new DateTime(2024, 10, 23, 8, 5, 0) },
            new() { Weight = 2.7m, CityRegion = "North", DeliveryDate = new DateTime(2024, 10, 23, 8, 30, 0) },
            new() { Weight = 1.9m, CityRegion = "North", DeliveryDate = new DateTime(2024, 10, 23, 8, 55, 0) },
            new() { Weight = 3.6m, CityRegion = "North", DeliveryDate = new DateTime(2024, 10, 23, 9, 20, 0) },
            new() { Weight = 2.8m, CityRegion = "North", DeliveryDate = new DateTime(2024, 10, 23, 9, 45, 0) },
            new() { Weight = 1.5m, CityRegion = "North", DeliveryDate = new DateTime(2024, 10, 23, 10, 10, 0) },
            new() { Weight = 3.8m, CityRegion = "North", DeliveryDate = new DateTime(2024, 10, 23, 10, 35, 0) },
            new() { Weight = 2.3m, CityRegion = "North", DeliveryDate = new DateTime(2024, 10, 23, 11, 0, 0) },

            new() { Weight = 2.5m, CityRegion = "South", DeliveryDate = new DateTime(2024, 10, 23, 8, 10, 0) },
            new() { Weight = 4.4m, CityRegion = "South", DeliveryDate = new DateTime(2024, 10, 23, 8, 35, 0) },
            new() { Weight = 3.4m, CityRegion = "South", DeliveryDate = new DateTime(2024, 10, 23, 9, 0, 0) },
            new() { Weight = 2.9m, CityRegion = "South", DeliveryDate = new DateTime(2024, 10, 23, 9, 25, 0) },
            new() { Weight = 4.0m, CityRegion = "South", DeliveryDate = new DateTime(2024, 10, 23, 9, 50, 0) },
            new() { Weight = 3.9m, CityRegion = "South", DeliveryDate = new DateTime(2024, 10, 23, 10, 15, 0) },
            new() { Weight = 2.2m, CityRegion = "South", DeliveryDate = new DateTime(2024, 10, 23, 10, 40, 0) },
            new() { Weight = 4.8m, CityRegion = "South", DeliveryDate = new DateTime(2024, 10, 23, 11, 5, 0) },

            new() { Weight = 1.8m, CityRegion = "West", DeliveryDate = new DateTime(2024, 10, 23, 8, 20, 0) },
            new() { Weight = 2.6m, CityRegion = "West", DeliveryDate = new DateTime(2024, 10, 23, 8, 45, 0) },
            new() { Weight = 2.1m, CityRegion = "West", DeliveryDate = new DateTime(2024, 10, 23, 9, 10, 0) },
            new() { Weight = 1.7m, CityRegion = "West", DeliveryDate = new DateTime(2024, 10, 23, 9, 35, 0) },
            new() { Weight = 2.4m, CityRegion = "West", DeliveryDate = new DateTime(2024, 10, 23, 10, 0, 0) },
            new() { Weight = 2.0m, CityRegion = "West", DeliveryDate = new DateTime(2024, 10, 23, 10, 25, 0) },
            new() { Weight = 1.6m, CityRegion = "West", DeliveryDate = new DateTime(2024, 10, 23, 10, 50, 0) },

            new() { Weight = 3.0m, CityRegion = "East", DeliveryDate = new DateTime(2024, 10, 23, 8, 25, 0) },
            new() { Weight = 3.1m, CityRegion = "East", DeliveryDate = new DateTime(2024, 10, 23, 8, 50, 0) },
            new() { Weight = 4.2m, CityRegion = "East", DeliveryDate = new DateTime(2024, 10, 23, 9, 15, 0) },
            new() { Weight = 3.3m, CityRegion = "East", DeliveryDate = new DateTime(2024, 10, 23, 9, 40, 0) },
            new() { Weight = 3.7m, CityRegion = "East", DeliveryDate = new DateTime(2024, 10, 23, 10, 5, 0) },
            new() { Weight = 4.3m, CityRegion = "East", DeliveryDate = new DateTime(2024, 10, 23, 10, 30, 0) },
            new() { Weight = 3.5m, CityRegion = "East", DeliveryDate = new DateTime(2024, 10, 23, 10, 55, 0) }
        ]);

        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        _connection.Close();
        _connection.Dispose();
    }
}
