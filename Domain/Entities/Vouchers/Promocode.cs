using Domain.Entities.Distances;

namespace Domain.Entities.Vouchers;

public class Promocode
{
    public int Id { get; set; }
    public string Code { get; set; }
    public bool IsActivated { get; set; } = false;
    public int DistanceId { get; set; }
    public Distance Distance { get; set; }
    public int VoucherId { get; set; }
    public Voucher Voucher { get; set; }
}
