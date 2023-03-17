using Core.Common.Bases;
using Domain.Entities.Marathons;

namespace Core.UseCases.Marathons.Queries.GetActiveMarathons;

public record GetActiveMarathonsOutDto : BaseDto<Marathon, GetActiveMarathonsOutDto>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public override void AddCustomMappings()
    {
        SetCustomMappings()
            .Map(x => x.Name, y => y.MarathonTranslations.First().Name);
    }

}
