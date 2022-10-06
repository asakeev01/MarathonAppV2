﻿using Domain.Entities.Marathons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.SavedFiles
{
    public class SavedFile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public int? PartnerId { get; set; }
        public virtual Partner? Partner { get; set; }
        public int? MarathonId { get; set; }

        public virtual Marathon? Marathon {get;set;}
        public virtual Marathon? MarathonLogo {get;set;}
    }
}
