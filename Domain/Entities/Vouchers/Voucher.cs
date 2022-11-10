using Domain.Entities.Marathons;

namespace Domain.Entities.Vouchers;

public class Voucher
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int MarathonId { get; set; }
    public   Marathon Marathon { get; set; }
    public ICollection<Promocode> Promocodes { get; set; }
}
