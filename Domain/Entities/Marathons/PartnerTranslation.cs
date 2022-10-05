using Domain.Entities.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Marathons
{
    public class PartnerTranslation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LanguageId { get; set; }
        public int PartnerId { get; set; }
        public Partner Partner { get; set; }
        public virtual Language Language { get; set; }
    }
}
