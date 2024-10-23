using System.ComponentModel.DataAnnotations;

namespace TestTask.Domain.Models;

public interface IDeliveryOrder
{
    public int Id { get; set; }
    public decimal Weight { get; set; }
    public string CityRegion { get; set; }
    public DateTime DeliveryDate { get; set; }
}
