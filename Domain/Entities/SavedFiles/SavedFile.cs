using Domain.Entities.Marathons;

namespace Domain.Entities.SavedFiles;

public class SavedFile
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Path { get; set; }
    public int? PartnerId { get; set; }
    public Partner? Partner { get; set; }
    public int? MarathonId { get; set; }

    public Marathon? Marathon {get;set;}
    public MarathonTranslation? MarathonLogo {get;set;}
}
