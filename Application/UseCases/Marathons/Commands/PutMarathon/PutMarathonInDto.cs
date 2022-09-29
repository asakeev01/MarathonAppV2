using Core.Common.Bases;
using Domain.Entities.Marathons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.UseCases.Marathons.Commands.PutMarathon
{
    public record PutMarathonInDto : BaseDto<PutMarathonInDto, Marathon>
    {
        public int Id { get; set; }
        public ICollection<TranslationDto> Translations { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartDateAcceptingApplications { get; set; }
        public DateTime EndDateAcceptingApplications { get; set; }
        public bool IsActive { get; set; }

        public override void AddCustomMappings()
        {
            SetCustomMappings()
                .Map(x => x.MarathonTranslations, x => x.Translations);
        }

        public class TranslationDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Text { get; set; }
            public string Place { get; set; }
            public int LanguageId { get; set; }
        }
    }
}
