using Domain.Entities.Applications;
using Domain.Entities.Marathons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Distances;

public class DistanceForPWD
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
    public Marathon Marathon { get; set; }
    public ICollection<Application> Applications { get; set; }
}
