using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Applications
{
    public class Application
    {
        public long Id { get; set; }
        public int DistanceId { get; set; }
        //[ForeignKey(nameof(DistanceId))]
        //public Distance Distance { get; set; }
    }
}
