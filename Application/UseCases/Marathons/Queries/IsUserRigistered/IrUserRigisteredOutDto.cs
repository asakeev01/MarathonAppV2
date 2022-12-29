using Core.Common.Bases;
using Domain.Entities.Applications;
using Domain.Entities.Applications.ApplicationEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.UseCases.Marathons.Queries.IsUserRigistered;

public record IrUserRigisteredOutDto : BaseDto<Application, IrUserRigisteredOutDto>
{
    public int Id { get; set; }
    public string Place { get; set; }
    public string? Distance { get; set; }
    public string? DistanceForPWD { get; set; }
    public string? Price { get; set; }
    public string? Paid { get; set; }
    public string? Voucher { get; set; }
    public DateTime ApplicationDate { get; set; }
    public DateTime MarathonDate { get; set; }
    public PaymentMethodEnum Payment { get; set; }


    public override void AddCustomMappings()
    {
        SetCustomMappings()
            .Map(x => x.Place, y => y.Marathon.MarathonTranslations.First().Place)
            .Map(x => x.Distance, y => y.Distance.Name)
            .Map(x => x.DistanceForPWD, y => y.DistanceForPWD.Name)
            .Map(x => x.Voucher, y => y.Promocode.Voucher.Name)
            .Map(x => x.ApplicationDate, y => y.Date)
            .Map(x => x.MarathonDate, y => y.Marathon.Date)
            ;
    }

}
