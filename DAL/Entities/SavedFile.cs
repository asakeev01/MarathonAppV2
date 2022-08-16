using MarathonApp.DAL.Entities;
using System.ComponentModel.DataAnnotations;

public class SavedFile
{
    public int  Id { get; set; }
    [MaxLength(128)]
    public string Name { get; set; }

    [MaxLength(512)]
    public string Path { get; set; }

    public virtual Partner Partner { get; set; }
}
