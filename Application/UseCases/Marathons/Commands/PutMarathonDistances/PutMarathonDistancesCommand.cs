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

namespace Core.UseCases.Marathons.Commands.PutMarathonDistances
{
    public class PutMarathonDistancesCommand : IRequest<HttpStatusCode>
    {
        public PutMarathonDistancesInDto marathonDto { get; set; }
    }

    public class PutMarathonDistancesCommandHandler : IRequestHandler<PutMarathonDistancesCommand, HttpStatusCode>
    {
        private readonly IUnitOfWork _unit;

        public PutMarathonDistancesCommandHandler(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task<HttpStatusCode> Handle(PutMarathonDistancesCommand cmd, CancellationToken cancellationToken)
        {
            var marathon = await _unit.MarathonRepository
            .FirstAsync(x => x.Id == cmd.marathonDto.Id, include: source => source
                .Include(a => a.Distances).ThenInclude(a => a.DistancePrices)
                .Include(a => a.Distances).ThenInclude(a => a.DistanceAges));

            cmd.marathonDto.Adapt(marathon);
            await _unit.MarathonRepository.SaveAsync();

            return HttpStatusCode.OK;
        }
    }
}
