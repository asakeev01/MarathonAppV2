using Core.Common.Bases;
using Domain.Entities.Marathons;
using Domain.Entities.SavedFiles;

namespace Core.UseCases.Marathons.Queries.GetMarathons
{
    public record GetMarathonsOutDto: BaseDto<Marathon, GetMarathonsOutDto>
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string Place { get; set; }
        public string Logo { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartDateAcceptingApplications { get; set; }
        public DateTime EndDateAcceptingApplications { get; set; }
        public bool IsActive { get; set; }

        public override void AddCustomMappings()
        {
            SetCustomMappings()
                .Map(x => x.Logo, y => y.Logo.Path)
                .Map(x => x.Name, y => y.MarathonTranslations.First().Name)
                .Map(x => x.Text, y => y.MarathonTranslations.First().Text)
                .Map(x => x.Place, y => y.MarathonTranslations.First().Place);

        }

    }
}
