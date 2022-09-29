using Core.Common.Bases;
using Domain.Entities.Distances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.UseCases.Distances.Queries.GetDistanceCategories
{
    public record GetDistanceCategoriesAdminOutDto : BaseDto<DistanceCategory, GetDistanceCategoriesAdminOutDto>
    {

        public int Id { get; set; }
        public virtual ICollection<DistanceCategoryTranslationDto> Translations { get; set; }

        public class DistanceCategoryTranslationDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public override void AddCustomMappings()
        {
            SetCustomMappings()
                .Map(x => x.Translations, y => y.DistanceCategoryTranslations);
        }
    }
}
