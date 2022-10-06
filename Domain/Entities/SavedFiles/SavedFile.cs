using Domain.Entities.Marathons;

namespace Domain.Entities.SavedFiles;

public class SavedFile
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }
    public int? PartnerId { get; set; }
    public virtual Partner? Partner { get; set; }
    public int? MarathonId { get; set; }

    public virtual Marathon? Marathon {get;set;}
    public virtual Marathon? MarathonLogo {get;set;}
}
