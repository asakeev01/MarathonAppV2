using Domain.Entities.Marathons;

namespace Domain.Entities.Vouchers;

public class Voucher
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool isActive { get; set; } = true;
    public int MarathonId { get; set; }
    public   Marathon Marathon { get; set; }
    public virtual ICollection<Promocode> Promocodes { get; set; }
}
