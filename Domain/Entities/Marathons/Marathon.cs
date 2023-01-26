using Domain.Entities.Applications;
using Domain.Entities.Distances;
using Domain.Entities.SavedFiles;
using Domain.Entities.Vouchers;

namespace Domain.Entities.Marathons;

public class Marathon
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public DateTime StartDateAcceptingApplications { get; set; }
    public DateTime EndDateAcceptingApplications { get; set; }
    public bool IsActive { get; set; }
    public ICollection<SavedFile>? Documents { get; set; }
    public ICollection<Distance> Distances { get; set; }
    public ICollection<MarathonTranslation> MarathonTranslations { get; set; }
    public ICollection<Partner>? Partners { get; set; }
    public ICollection<Voucher>? Vouchers { get; set; }
    public ICollection<Application> Applications { get; set; }
}
