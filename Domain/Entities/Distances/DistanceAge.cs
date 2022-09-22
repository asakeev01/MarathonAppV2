using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Distances
{
    public class DistanceAge
    {
        public int Id { get; set; }
        public int? AgeFrom { get; set; }
        public int? AgeTo { get; set; }
        public int DistanceId { get; set; }
        public virtual Distance Distance { get; set; }
    }
}
