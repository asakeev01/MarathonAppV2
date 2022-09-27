﻿using Core.Common.Bases;
using Domain.Entities.Marathons;

namespace Core.UseCases.Marathons.Queries.GetMarathon;

    public record GetMarathonOutDto: BaseDto<Marathon, GetMarathonOutDto>
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
                .Map(x => x.Name, y => y.MarathonTranslations.First().Name)
                .Map(x => x.Text, y => y.MarathonTranslations.First().Text)
                .Map(x => x.Place, y => y.MarathonTranslations.First().Place);

        }
    }

