using Domain.Entities.Distances;
using Domain.Entities.Marathons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Languages
{
    public class Language
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public virtual ICollection<MarathonTranslation> MarathonTranslations { get; set; }
    }
}
