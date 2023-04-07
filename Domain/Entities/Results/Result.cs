using Domain.Entities.Applications;

namespace Domain.Entities.Results;

public class Result
{
    public int Id { get; set; }
    public string CategoryPlace { get; set; }
    public string GeneralPlace { get; set; }
    public int CategoryCount { get; set; }
    public int GeneralCount { get; set; }
    public string GunTime { get; set; }
    public string ChipTime { get; set; }
    public string? Json { get; set; }
    public int ApplicationId { get; set; }
    public Application Application { get; set; }
}
