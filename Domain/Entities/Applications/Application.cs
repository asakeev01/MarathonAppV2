using Domain.Entities.Applications.ApplicationEnums;
using Domain.Entities.Distances;
using Domain.Entities.Marathons;
using Domain.Entities.Users;
using Domain.Entities.Vouchers;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Applications;

public class Application
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int Number { get; set; }
    public bool StarterKit { get; set; }
    public PaymentMethodEnum Payment { get; set; }
    public int DistanceId { get; set; }
    public long UserId { get; set; }
    public int MarathonId { get; set; }
    public int? PromocodeId { get; set; }
    public Promocode? Promocode { get; set; }
    public virtual Distance Distance { get; set; }
    public virtual DistanceAge DistanceAge { get; set; } 
    public virtual User User { get; set; }
    public virtual Marathon? Marathon { get; set; }

}
