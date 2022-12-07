using Core.Common.Bases;
using Domain.Entities.Vouchers;


namespace Core.UseCases.Vouchers.Queries.GetVouchers;

public record GetVourchersQueryOutDto : BaseDto<Voucher, GetVourchersQueryOutDto>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool isActive { get; set; }
    public int MarathonId { get; set; }
    public int TotalPromocodes { get; set; }
    public int UsedPromocodes { get; set; }

    public override void AddCustomMappings()
    {
        SetCustomMappings()
            .Map(x => x.TotalPromocodes, y => y.Promocodes.Count)
            .Map(x => x.UsedPromocodes, y => y.Promocodes.Count(z => z.IsActivated == true));
    }
}