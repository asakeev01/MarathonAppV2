using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common.Extensions.SwaggerServices;

namespace WebApi.Endpoints.Applications.Dtos.Requests;

public class ImportExcelApplicationsRequestDto
{
    public IFormFile ExcelFile { get; set; }
}

public class ImportExcelApplicationsRequestDtoValidator : AbstractValidator<ImportExcelApplicationsRequestDto>
{
    public ImportExcelApplicationsRequestDtoValidator()
    {
        RuleFor(x => x.ExcelFile.ContentType).Must(x => x.Equals("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"))
            .WithMessage("Only excel are allowed");
        RuleFor(x => x.ExcelFile.Length).NotNull().LessThanOrEqualTo(40 * 1024 * 1024)
            .WithMessage("File size is larger than allowed");
    }

}
