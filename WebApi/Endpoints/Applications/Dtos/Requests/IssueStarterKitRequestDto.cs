using System;
using Domain.Entities.Applications.ApplicationEnums;
using FluentValidation;

namespace WebApi.Endpoints.Applications.Dtos.Requests;

public class IssueStarterKitRequestDto
{
    public StartKitEnum StarterKit { get; set; }
    public string? FullNameRecipient { get; set; }
}

public class IssueStarterKitRequestDtoValidator : AbstractValidator<IssueStarterKitRequestDto>
{
    public IssueStarterKitRequestDtoValidator()
    {
    }

}

