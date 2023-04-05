using System;
using Core.Common.Helpers;
using Domain.Common.Contracts;
using Gridify;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.UseCases.Results.Queries.PrintResult;

public class PrintResultQuery : IRequest<PrintResultOutDto>
{
    public int ResultId { get; set; }
}

public class PrintResultHandler : IRequestHandler<PrintResultQuery, PrintResultOutDto>
{
    private readonly IUnitOfWork _unit;

    public PrintResultHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<PrintResultOutDto> Handle(PrintResultQuery request,
        CancellationToken cancellationToken)
    {

        var result = await _unit.ResultRepository.FirstToAsync<PrintResultOutDto>(x => x.Id == request.ResultId, include: source => source
        .Include(x => x.Application).ThenInclude(x => x.DistanceAge)
        .Include(x => x.Application).ThenInclude(x => x.User)
        .Include(x => x.Application).ThenInclude(x => x.Marathon)
        .Include(x => x.Application).ThenInclude(x => x.Distance).ThenInclude(x => x.Applications).ThenInclude(x => x.User)
        );

        return result;
    }
}
