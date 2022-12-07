using FluentValidation;

namespace WebApi.Endpoints.Vouchers.Dtos.Requests;

public class UpdateVoucherRequestDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
}


public class UpdateVoucherRequestValidator : AbstractValidator<UpdateVoucherRequestDto>
{
    public UpdateVoucherRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.IsActive).NotNull();
    }
}
