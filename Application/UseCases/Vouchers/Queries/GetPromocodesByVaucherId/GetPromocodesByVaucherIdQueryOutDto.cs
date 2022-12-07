using Core.Common.Bases;
using Domain.Entities.Vouchers;

namespace Core.UseCases.Vouchers.Queries.GetVouchers;

public record GetPromocodesByVaucherIdQueryOutDto : BaseDto<Promocode, GetPromocodesByVaucherIdQueryOutDto>
{
    public int Id { get; set; }
    public string Code { get; set; }
    public bool IsActivated { get; set; }
    public DateTime CreationDate { get; set; }
    public DistanceDto Distance { get; set; }
    public UserDto User { get; set; }
        

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