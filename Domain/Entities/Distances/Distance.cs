using Domain.Entities.Applications;
using Domain.Entities.Marathons;
using Domain.Entities.Vouchers;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Distances;

public class Distance
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int StartNumbersFrom { get; set; }
    public int StartNumbersTo { get; set; }
    public int InitializedPlaces { get; set; } = 0;
    public int ReservedPlaces { get; set; } = 0;
    public int ActivatedReservedPlaces { get; set; } = 0;
    [NotMapped]
    public int AmountOfParticipants
    {
        get
        {
            return StartNumbersTo - StartNumbersFrom + 1;
        }
    }
    [NotMapped]
    public int RemainingPlaces
    {
        get
        {
            return AmountOfParticipants - RegisteredParticipants - ReservedPlaces - InitializedPlaces;
        }
    }
    public int RegisteredParticipants { get; set; } = 0;
    public int MarathonId { get; set; }
    public Marathon Marathon { get; set; }
    public ICollection<DistancePrice> DistancePrices { get; set; }
    public ICollection<DistanceAge> DistanceAges { get; set; }
    public ICollection<Promocode> Promocodes { get; set; }
    public ICollection<Application> Applications { get; set; }
}
