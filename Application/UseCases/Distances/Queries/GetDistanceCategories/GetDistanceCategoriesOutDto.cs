using Core.Common.Bases;
using Domain.Entities.Distances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.UseCases.Distances.Queries.GetDistanceCategories
{
    public record GetDistanceCategoriesOutDto : BaseDto<DistanceCategory, GetDistanceCategoriesOutDto>
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public override void AddCustomMappings()
        {
            SetCustomMappings()
                .Map(x => x.Name, y => y.DistanceCategoryTranslations.First().Name);
        }
    }
}
