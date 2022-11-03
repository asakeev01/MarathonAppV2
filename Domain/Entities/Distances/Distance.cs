﻿using Domain.Entities.Marathons;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Distances;

public class Distance
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int StartNumbersFrom { get; set; }
    public int StartNumbersTo { get; set; }
    [NotMapped]
    public int AmountOfParticipants
    {
        get
        {
            return StartNumbersTo - StartNumbersFrom;
        }
    }
    [NotMapped]
    public int RemainingPlaces
    {
        get
        {
            return AmountOfParticipants - RegisteredParticipants;
        }
    }
    public int RegisteredParticipants { get; set; } = 0;
    public int MarathonId { get; set; }
    public virtual Marathon Marathon { get; set; }
    public virtual ICollection<DistancePrice> DistancePrices { get; set; }
    public virtual ICollection<DistanceAge> DistanceAges { get; set; }
}
