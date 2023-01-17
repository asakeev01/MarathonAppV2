using Core.Common.Bases;
using Domain.Entities.Vouchers;
using Gridify;

namespace Core.UseCases.Vouchers.Queries.GetVouchers;

public record GetPromocodesByVaucherIdQueryOutDto : BaseDto<Voucher, GetPromocodesByVaucherIdQueryOutDto>
{
    public string VoucherName { get; set; }
    public QueryablePaging<PromocodeDto> Promocodes { get; set; }


    public record PromocodeDto : BaseDto<Promocode, PromocodeDto>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public bool IsActivated { get; set; }
        public DateTime CreationDate { get; set; }
        public string Distance { get; set; }
        public UserDto User { get; set; }

        public override void AddCustomMappings()
        {
            SetCustomMappings()
                .Map(x => x.Distance, y => y.Distance.Name);
        }
    }
        

    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
    }
    public class DistanceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
        

}