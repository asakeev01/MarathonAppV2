using Domain.Entities.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Marathons
{
    public class MarathonTranslation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string Place { get; set; }
        public int LanguageId { get; set; }
        public int MarathonId { get; set; }
        public Marathon Marathon { get; set; }
        public virtual Language Language { get; set; }
    }
}
