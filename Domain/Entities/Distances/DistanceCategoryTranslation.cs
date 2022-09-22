using Domain.Entities.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Distances
{
    public class DistanceCategoryTranslation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LanguageId { get; set; }
        public virtual Language Language { get; set; }
        public virtual DistanceCategory DistanceCategory { get; set; }
    }
}
