﻿namespace Domain.Entities.Distances;

public class DistanceAge
{
    public int Id { get; set; }
    public bool Gender { get; set; }
    public int? AgeFrom { get; set; }
    public int? AgeTo { get; set; }
    public int DistanceId { get; set; }
    public Distance Distance { get; set; }
}
