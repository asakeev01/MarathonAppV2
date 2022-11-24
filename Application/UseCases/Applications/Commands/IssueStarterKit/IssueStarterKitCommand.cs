using System;
using System.Net;
using Domain.Common.Contracts;
using Domain.Entities.Applications.ApplicationEnums;
using Domain.Services.Interfaces;
using MediatR;

namespace Core.UseCases.Applications.Commands.IssueStarterKit;

public class IssueStarterKitCommand : IRequest<HttpStatusCode>
{
    public StartKitEnum StarterKit { get; set; }
    public int ApplicationId { get; set; }
    public string? FullNameRecipient { get; set; }
}

public class CreateApplicationHandler : IRequestHandler<IssueStarterKitCommand, HttpStatusCode>
{
    private readonly IUnitOfWork _unit;
    private readonly IApplicationService _applicationService;
    private readonly IEmailService _emailService;

    public CreateApplicationHandler(IUnitOfWork unit, IApplicationService applicationService, IEmailService emailService)
    {
        _unit = unit;
        _applicationService = applicationService;
        _emailService = emailService;
    }

    public async Task<HttpStatusCode> Handle(IssueStarterKitCommand cmd, CancellationToken cancellationToken)
    {
        var application = await _unit.ApplicationRepository.FirstAsync(a => a.Id == cmd.ApplicationId);
        application = _applicationService.IssueStarterKit(application, cmd.FullNameRecipient, cmd.StarterKit);
        await _unit.ApplicationRepository.SaveAsync();
        return HttpStatusCode.OK;
    }
}

