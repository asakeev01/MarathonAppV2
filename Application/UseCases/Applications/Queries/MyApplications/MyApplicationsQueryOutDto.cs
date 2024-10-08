﻿using Core.Common.Bases;
using Domain.Entities.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.UseCases.Applications.Queries.MyApplications
{
    public record MyApplicationsQueryOutDto : BaseDto<Application, MyApplicationsQueryOutDto>
    {
        public string MarathonName { get; set; }
        public int MarathonId { get; set; }
        public DateTime Date { get; set; }
        public string Distance { get; set; }
        public string Place { get; set; }
        public int? AgeFrom { get; set; }
        public int? AgeTo { get; set; }
        public bool IsPWD { get; set; }
        public string StarterKitCode { get; set; }
        public int Number { get; set; }

        public override void AddCustomMappings()
        {
            SetCustomMappings()
                .Map(x => x.MarathonName, y => y.Marathon.MarathonTranslations.First().Name)
                .Map(x => x.MarathonId, y => y.Marathon.Id)
                .Map(x => x.Date, y => y.Marathon.Date)
                .Map(x => x.Distance, y => y.Distance.Name)
                .Map(x => x.Place, y => y.Marathon.MarathonTranslations.First().Place)
                .Map(x => x.AgeFrom, y => y.DistanceAge.AgeFrom)
                .Map(x => x.AgeTo, y => y.DistanceAge.AgeTo);
        }

    }
}
