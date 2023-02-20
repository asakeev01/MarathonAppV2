using Domain.Entities.Marathons;

namespace Domain.Entities.Languages;

public class Language
{
    public int Id { get; set; }
    public string Code { get; set; }
    public ICollection<MarathonTranslation> MarathonTranslations { get; set; }
    public ICollection<PartnerTranslation> PartnerTranlations { get; set; }
}
