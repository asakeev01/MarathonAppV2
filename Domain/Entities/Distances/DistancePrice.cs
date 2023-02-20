using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Distances;

public class DistancePrice
{
    public int Id { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }
    [Column(TypeName = "decimal(18,4)")]
    public decimal Price { get; set; }
    public int DistanceId { get; set; }
    public Distance Distance { get; set; }
}
