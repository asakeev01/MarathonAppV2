using Domain.Entities.Languages;

namespace Domain.Entities.Marathons;

public class PartnerTranslation
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int LanguageId { get; set; }
    public int PartnerId { get; set; }
    public Partner Partner { get; set; }
    public Language Language { get; set; }
}
