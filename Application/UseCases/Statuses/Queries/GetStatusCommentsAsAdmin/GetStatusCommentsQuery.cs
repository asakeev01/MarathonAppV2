using System;
using Domain.Common.Contracts;
using Gridify;
using Mapster;
using MediatR;

namespace Core.UseCases.Statuses.Queries.GetStatusCommentsAsAdmin;

public class GetStatusCommentsQuery : IRequest<QueryablePaging<GetStatusCommentsOutDto>>
{
    public GridifyQuery Query { get; set; }
}

public class GetUsersHandler : IRequestHandler<GetStatusCommentsQuery, QueryablePaging<GetStatusCommentsOutDto>>
{
    private readonly IUnitOfWork _unit;

    public GetUsersHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<QueryablePaging<GetStatusCommentsOutDto>> Handle(GetStatusCommentsQuery request, CancellationToken cancellationToken)
    {
        var comments = await _unit.CommentRepository.GetAllAsync();
        var response = comments.Adapt<IEnumerable<GetStatusCommentsOutDto>>().AsQueryable().GridifyQueryable(request.Query);
        return response;
    }
}


