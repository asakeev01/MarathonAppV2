using Core.Common.Bases;
using Domain.Entities.Marathons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.UseCases.Marathons.Queries.GetMarathons
{
    public record GetMarathonOUTDTO: BaseDto<Marathon, GetMarathonOUTDTO>
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string Place { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartDateAcceptingApplications { get; set; }
        public DateTime EndDateAcceptingApplications { get; set; }
        public bool IsActive { get; set; }
        //public ICollection<MarathonTranslationDto> MarathonTranslations { get; set; }

        public override void AddCustomMappings()
        {
            SetCustomMappings()
                .Map(x => x.Name, y => y.MarathonTranslations.First().Name)
                .Map(x => x.Text, y => y.MarathonTranslations.First().Text)
                .Map(x => x.Place, y => y.MarathonTranslations.First().Place);

        }

        public class MarathonTranslationDto
        {
            public string Name { get; set; }
            public string Text { get; set; }
            public string  Place { get; set; }

        }
    }
}
