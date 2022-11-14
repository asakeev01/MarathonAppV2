using FluentValidation;

namespace WebApi.Endpoints.Vouchers.Dtos.Requests;


public class AddPromocodesToVoucherRequestDto
{
    public ICollection<PromocodeDto> Promocodes { get; set; }

    public class PromocodeDto
    {
        public int DistanceId { get; set; }
        public int Quantity { get; set; }
    }
}


public class AddPromocodesToVoucherRequestValidator : AbstractValidator<AddPromocodesToVoucherRequestDto>
{
    public AddPromocodesToVoucherRequestValidator()
    {
        RuleForEach(x => x.Promocodes).ChildRules(promocodes =>
        {
            promocodes.RuleFor(x => x.DistanceId).NotNull();
            promocodes.RuleFor(x => x.Quantity).GreaterThan(0);
        });
    }
}
