using System.ComponentModel.DataAnnotations;

namespace TestTask.Domain.Models;

public class FetchedDeliveryOrder : IDeliveryOrder
{
    [Key]
    public int Id { get; set; }

    public decimal Weight { get; set; }

    [Required, StringLength(50)]
    public string CityRegion { get; set; }

    public DateTime DeliveryDate { get; set; }

    [Required, StringLength(50)]
    public string RegionQuery {  get; set; }

    public DateTime FirstOrderDateQuery { get; set; }

    public DateTime QueryDate { get; set;}
}
