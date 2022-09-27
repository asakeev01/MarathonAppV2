using Core.Common.Bases;
using Domain.Entities.Marathons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.UseCases.Marathons.Queries.GetMarathon
{
    public record GetMarathonsOutDto : BaseDto<MarathonTranslation, GetMarathonsOutDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string Place { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartDateAcceptingApplications { get; set; }
        public DateTime EndDateAcceptingApplications { get; set; }
        public bool IsActive { get; set; }


        public override void AddCustomMappings()
        {
            SetCustomMappings()
                .Map(x => x.Id, y => y.Marathon.Id)
                .Map(x => x.Date, y => y.Marathon.Date)
                .Map(x => x.StartDateAcceptingApplications, y => y.Marathon.StartDateAcceptingApplications)
                .Map(x => x.EndDateAcceptingApplications, y => y.Marathon.EndDateAcceptingApplications)
                .Map(x => x.IsActive, y => y.Marathon.IsActive);

        }
    }
}
