using FluentValidation;

namespace WebApi.Endpoints.Results.Dtos;

public class SetResultsByExcelDto
{
    public IFormFile ExcelFile { get; set; }
}

public class SetResultsByExcelDtoValidator : AbstractValidator<SetResultsByExcelDto>
{
    public SetResultsByExcelDtoValidator()
    {
        RuleFor(x => x.ExcelFile.ContentType).Must(x => x.Equals("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"))
            .WithMessage("Only excel are allowed");
        RuleFor(x => x.ExcelFile.Length).NotNull().LessThanOrEqualTo(40 * 1024 * 1024)
            .WithMessage("File size is larger than allowed");
    }

}
