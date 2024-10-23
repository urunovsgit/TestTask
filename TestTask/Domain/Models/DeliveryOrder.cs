using System.ComponentModel.DataAnnotations;

namespace TestTask.Domain.Models;

public class DeliveryOrder
{
    [Key]
    public int Id { get; set; }

    public decimal Weight { get; set; }

    [Required, StringLength(50)]
    public string CityRegion { get; set; }

    public DateTime DeliveryDate { get; set; }
}
