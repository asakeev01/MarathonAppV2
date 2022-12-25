using Domain.Entities.Distances;
using Domain.Entities.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Vouchers;

public class Promocode
{
    public int Id { get; set; }
    public string Code { get; set; }
    public bool IsActivated { get; set; } = false;
    public DateTime CreationDate { get; set; }
    public int DistanceId { get; set; }
    public Distance Distance { get; set; }
    public int VoucherId { get; set; }
    public Voucher Voucher { get; set; }
    public long? UserId { get; set; }
    public User? User { get; set; }
}
