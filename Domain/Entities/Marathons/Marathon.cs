using Domain.Entities.Distances;
using Domain.Entities.SavedFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public virtual ICollection<SavedFile>? Documents { get; set; }
        public virtual ICollection<Distance> Distances { get; set; }
        public virtual SavedFile? Logo { get; set; }
        public int? LogoId { get; set; }
        public ICollection<MarathonTranslation> MarathonTranslations { get; set; }
        public ICollection<Partner>? Partners { get; set; }
    }
}
