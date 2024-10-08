﻿using Core.Common.Bases;
using Domain.Entities.Results;
using System.Text.RegularExpressions;

namespace Core.UseCases.Results.Queries.PrintResult;

public record PrintResultOutDto : BaseDto<Result, PrintResultOutDto>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string CategoryPlace { get; set; }
    public string GeneralPlace { get; set; }
    public int CategoryCount { get; set; }
    public int GeneralCount { get; set; }
    public string GunTime { get; set; }
    public string ChipTime { get; set; }
    public string Distance { get; set; }
    public string DistanceAge { get; set; }
    public string Number { get; set; }
    public string Gender { get; set; }

    public override void AddCustomMappings()
    {
        SetCustomMappings()
            .Map(x => x.Number, y => y.Application.Number)
            .Map(x => x.Gender, y => y.Application.User.Gender == true ? "M" : "F")
            .Map(x => x.Name, y => y.Application.User.Name)
            .Map(x => x.Surname, y => y.Application.User.Surname)
            .Map(x => x.Distance, y => y.Application.Distance.Name)
            .Map(x => x.DistanceAge, y => y.Application.DistanceAgeId == null ? "PWD" : $"{y.Application.DistanceAge.AgeFrom}-{y.Application.DistanceAge.AgeTo}")
            //.Map(x => x.CategoryCount, y => y.Application.DistanceAgeId == null ? y.Application.Distance.Applications.Where(x => x.DistanceId == y.Application.DistanceId && x.IsPWD == true && y.Application.User.Gender == x.User.Gender).Count() : y.Application.Distance.Applications.Where(x => x.DistanceId == y.Application.DistanceId && x.DistanceAgeId == y.Application.DistanceAgeId).Count())
            //.Map(x => x.GeneralCount, y => y.Application.DistanceAgeId == null ? y.Application.Distance.Applications.Where(x => x.DistanceId == y.Application.DistanceId && x.IsPWD == true).Count() : y.Application.Distance.Applications.Where(x => x.DistanceId == y.Application.DistanceId && x.IsPWD != true).Count())
            ;
    }
}
