using Core.Common.Bases;
using Domain.Entities.Marathons;
using Domain.Entities.Vouchers;


namespace Core.UseCases.Vouchers.Queries.GetVouchers;

public record GetVourchersQueryOutDto : BaseDto<Marathon, GetVourchersQueryOutDto>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public DateTime StartDateAcceptingApplications { get; set; }
    public DateTime EndDateAcceptingApplications { get; set; }
    public bool IsActive { get; set; }
    public ICollection<VoucherDto>? Vouchers { get; set; }

    public record VoucherDto : BaseDto<Voucher, VoucherDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool isActive { get; set; }
        public int TotalPromocodes { get; set; }
        public int UsedPromocodes { get; set; }

        public override void AddCustomMappings()
        {
            SetCustomMappings()
                .Map(x => x.TotalPromocodes, y => y.Promocodes.Count)
                .Map(x => x.UsedPromocodes, y => y.Promocodes.Count(z => z.IsActivated == true));
            //SetCustomMappings()
            //    .Map(x => x.TotalPromocodes, y => 0)
            //    .Map(x => x.UsedPromocodes, y => -1);
        }
    }

    public override void AddCustomMappings()
    {
        SetCustomMappings()
            .Map(x => x.Name, y => y.MarathonTranslations.First().Name);
    }
}