using Core.Common.Bases;
using Domain.Entities.Marathons;

namespace Core.UseCases.Marathons.Commands.AddPartner;

public record AddPartnerCommandInDto : BaseDto<AddPartnerCommandInDto, Partner>
{
    public ICollection<TrasnlationDto> Translations { get; set; }

    public class TrasnlationDto
    {
        public string Name { get; set; }
        public int LanguageId { get; set; }
    }
}
