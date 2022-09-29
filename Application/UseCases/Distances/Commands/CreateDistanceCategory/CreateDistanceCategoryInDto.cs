using Core.Common.Bases;
using Domain.Entities.Distances;
using Domain.Entities.Marathons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.UseCases.Distances.Commands.CreateDistanceCategory
{
    public record CreateDistanceCategoryInDto : BaseDto<CreateDistanceCategoryInDto, DistanceCategory>
    {
        public ICollection<DistanceCategoryTranslationDto> Translations { get; set; }
        public override void AddCustomMappings()
        {
            SetCustomMappings()
                .Map(x => x.DistanceCategoryTranslations, y => y.Translations);
        }
        public class DistanceCategoryTranslationDto
        {
            public string Name { get; set; }
            public int LanguageId { get; set; }
        }

    }
}