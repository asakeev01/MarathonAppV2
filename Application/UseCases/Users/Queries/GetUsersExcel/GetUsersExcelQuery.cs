using System;
using Core.Common.Helpers;
using Domain.Common.Contracts;
using Domain.Entities.Users.Constants;
using Gridify;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.UseCases.Users.Queries.GetUsersExcel;

public class GetUsersExcelQuery : IRequest<byte[]>
{
    public GridifyQuery Query { get; set; }
    public string LanguageCode { get; set; }
}

public class GetUsersExcelHandler : IRequestHandler<GetUsersExcelQuery, byte[]>
{
    private readonly IUnitOfWork _unit;

    public GetUsersExcelHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<byte[]> Handle(GetUsersExcelQuery request, CancellationToken cancellationToken)
    {
        var users = await _unit.UserRepository.GetAllAsync(include: source => source
        .Include(x => x.Documents)
        .Include(x => x.Applications).ThenInclude(y => y.DistanceAge)
        .Include(x => x.Applications).ThenInclude(y => y.Marathon).ThenInclude(z => z.MarathonTranslations)
        );

        var byte_excel = await _unit.UserRepository.GenerateExcel(users);

        return (byte_excel);
    }
}

