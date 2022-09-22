using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Distances
{
    public class DistanceCategory
    {
        public int Id { get; set; }
        public virtual ICollection<Distance> Distances { get; set; }
        public virtual ICollection<DistanceCategoryTranslation> DistanceCategoryTranslations { get; set; }

    }
}
