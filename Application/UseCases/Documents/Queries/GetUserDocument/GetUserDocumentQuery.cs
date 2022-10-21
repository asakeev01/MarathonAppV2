using System;
using Domain.Common.Contracts;
using Mapster;
using MediatR;

namespace Core.UseCases.Documents.Queries.GetUserDocument
{
    public class GetUserDocumentQuery : IRequest<GetUserDocumentOutDto>
    {
        public string? userId { get; set; }
    }

    public class GetUserDocumentHandler : IRequestHandler<GetUserDocumentQuery, GetUserDocumentOutDto>
    {
        private readonly IUnitOfWork _unit;

        public GetUserDocumentHandler(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task<GetUserDocumentOutDto> Handle(GetUserDocumentQuery request, CancellationToken cancellationToken)
        {
            var document = await _unit.DocumentRepository.FirstAsync(x => x.UserId == long.Parse(request.userId));
            var userDocumentDto = document.Adapt<GetUserDocumentOutDto>();
            return userDocumentDto;
        }
    }
}

