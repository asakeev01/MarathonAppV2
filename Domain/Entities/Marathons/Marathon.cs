using Domain.Entities.Distances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Marathons
{
    public class Marathon
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartDateAcceptingApplications { get; set; }
        public DateTime EndDateAcceptingApplications { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Distance> Distances { get; set; }
        public virtual ICollection<MarathonTranslation> MarathonTranslations { get; set; }
    }
}
