using System.Net;
using Core.UseCases.Marathons.Commands.CreateMarathon;
using Domain.Common.Constants;
using Domain.Common.Contracts;
using Domain.Common.Exceptions;
using Domain.Entities.Marathons;
using Domain.Entities.Marathons.Exceptions;
using Domain.Services.Interfaces;
using Mapster;
using MediatR;
using Microsoft.Data.SqlClient;

namespace Core.UseCases.Marathons.Commands.CraeteMarathon;

public class CreateMarathonCommand : IRequest<int>
{
    public CreateMarathonRequestInDto marathonDto { get; set; }
}

public class CreateMarathonCommandHandler : IRequestHandler<CreateMarathonCommand, int>
{
    private readonly IAccountService _accountService;
    private readonly IUnitOfWork _unit;

    public CreateMarathonCommandHandler(IAccountService accountService, IUnitOfWork unit)
    {
        _accountService = accountService;
        _unit = unit;
    }

    public async Task<int> Handle(CreateMarathonCommand cmd, CancellationToken cancellationToken)
    {
        try
        {
            var ints = cmd.marathonDto.Translations.Select((o) => o.LanguageId).OrderBy(x => x).ToArray();
            var ii = AppConstants.SupportedLanguagesIds;
            var b = ints.SequenceEqual(ii);
            var entity = cmd.marathonDto.Adapt<Marathon>();
            var marathon = await _unit.MarathonRepository.CreateAsync(entity, save: true);
            return marathon.Id;
        }
        catch (SqlException e) when (e.Number == 2601)
        {
            throw new MarathonTranslationIndexException();
        }
 
    }
}