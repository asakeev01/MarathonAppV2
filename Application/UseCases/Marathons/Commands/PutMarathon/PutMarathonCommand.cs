using Domain.Common.Contracts;
using Domain.Services.Interfaces;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.UseCases.Marathons.Commands.PutMarathon
{
    public class PutMarathonCommand : IRequest<HttpStatusCode>
    {
        public PutMarathonInDto marathonDto { get; set; }
    }

    public class PutMarathonCommandHandler : IRequestHandler<PutMarathonCommand, HttpStatusCode>
    {
        private readonly IUnitOfWork _unit;

        public PutMarathonCommandHandler(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task<HttpStatusCode> Handle(PutMarathonCommand cmd, CancellationToken cancellationToken)
        {
            var marathon = await _unit.MarathonRepository
            .FirstAsync(x => x.Id == cmd.marathonDto.Id, include: source => source.Include(a => a.MarathonTranslations));

            cmd.marathonDto.Adapt(marathon);
            await _unit.MarathonRepository.SaveAsync();

            return HttpStatusCode.OK;
        }
    }
}
