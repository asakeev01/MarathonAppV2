using Domain.Entities.SavedFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Marathons
{
    public class Partner
    {
        public int Id { get; set; }
        public int MarathonId { get; set; }
        public virtual Marathon Marathon { get; set; }
        public virtual ICollection<PartnerTranslation> Translations { get; set; }
        public virtual ICollection<SavedFile> Logos{get;set;}
    }
}
