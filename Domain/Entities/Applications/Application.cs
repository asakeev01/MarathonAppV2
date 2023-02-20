using Domain.Entities.Applications.ApplicationEnums;
using Domain.Entities.Distances;
using Domain.Entities.Marathons;
using Domain.Entities.Users;
using Domain.Entities.Vouchers;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Applications;

public class Application
{
    public int Id { get; set; }
    public string? Magnet { get; set; }
    public DateTime Date { get; set; }
    public int Number { get; set; }
    public StartKitEnum StarterKit { get; set; }
    public PaymentMethodEnum Payment { get; set; }
    [Column(TypeName = "decimal(18,4)")]
    public decimal? Price { get; set; }
    [Column(TypeName = "decimal(18,4)")]
    public decimal? Paid { get; set; }
    public string StarterKitCode { get; set; }
    public string? FullNameRecipient { get; set; }
    public DateTime? DateOfIssue { get; set; }
    public DateTime? RemovalTime { get; set; }
    public bool IsPWD { get; set; }
    public int? DistanceAgeId { get; set; }
    public int? DistanceId { get; set; }
    public long UserId { get; set; }
    public int MarathonId { get; set; }
    public int? PromocodeId { get; set; }
    public int? PaymentId { get; set; }
    public string? PaymentUrl { get; set; }
    public Promocode? Promocode { get; set; }
    public Distance? Distance { get; set; }
    public DistanceAge? DistanceAge { get; set; }
    public User User { get; set; }
    public Marathon? Marathon { get; set; }
}
