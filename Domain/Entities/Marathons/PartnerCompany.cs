using Domain.Entities.SavedFiles;

namespace Domain.Entities.Marathons;

public class PartnerCompany
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public int? LogoId { get; set; }
    public SavedFile? Logo { get; set; }   
    public Partner Partner { get; set; }
}
