using Domain.Entities.Distances;
using Domain.Entities.SavedFiles;

namespace Domain.Entities.Marathons;

public class Marathon
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public DateTime StartDateAcceptingApplications { get; set; }
    public DateTime EndDateAcceptingApplications { get; set; }
    public bool IsActive { get; set; }
    public virtual ICollection<SavedFile>? Documents { get; set; }
    public virtual ICollection<Distance> Distances { get; set; }
    public virtual ICollection<DistanceForPWD> DistancesForPWD { get; set; }
    public ICollection<MarathonTranslation> MarathonTranslations { get; set; }
    public ICollection<Partner>? Partners { get; set; }
}
