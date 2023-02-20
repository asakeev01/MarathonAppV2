using Core.Common.Bases;
using Domain.Entities.Vouchers;

namespace Core.UseCases.Vouchers.Commands.CreateVoucher;

public record CreateVoucherInDto : BaseDto<CreateVoucherInDto, Voucher>
{
    public string Name { get; set; }
    public int MarathonId { get; set; }
    public ICollection<PromocodeDto> Promocodes { get; set; }

    public override void AddCustomMappings()
    {
        SetCustomMappings()
            .Ignore(x => x.Promocodes);
    }

    public class PromocodeDto
    {
        public int DistanceId { get; set; }
        public int Quantity { get; set; }
    }
}

