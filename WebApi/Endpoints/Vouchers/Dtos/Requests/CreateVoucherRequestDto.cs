using FluentValidation;

namespace WebApi.Endpoints.Vouchers.Dtos.Requests;

public class CreateVoucherRequestDto
{
    public string Name { get; set; }
    public int MarathonId { get; set; }
    public ICollection<PromocodeDto> Promocodes { get; set; }

    public class PromocodeDto
    {
        public int DistanceId { get; set; }
        public int Quantity { get; set; }
    }
}


public class CreateVoucherRequestValidator : AbstractValidator<CreateVoucherRequestDto>
{
    public CreateVoucherRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Name).NotNull();
        RuleFor(x => x.MarathonId).NotNull();
        RuleForEach(x => x.Promocodes).ChildRules(promocodes =>
        {
            promocodes.RuleFor(x => x.DistanceId).NotNull();
            promocodes.RuleFor(x => x.Quantity).GreaterThan(0);
        });
    }
}
