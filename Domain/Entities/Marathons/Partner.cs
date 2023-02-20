using Domain.Entities.SavedFiles;

namespace Domain.Entities.Marathons;

public class Partner
{
    public int Id { get; set; }
    public int SerialNumber { get; set; }
    public int MarathonId { get; set; }
    public Marathon Marathon { get; set; }
    public ICollection<PartnerTranslation> Translations { get; set; }
    public ICollection<PartnerCompany>? PartnerCompanies { get;set;}
}
