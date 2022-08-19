using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MarathonApp.DAL.Entities;

namespace Models.Applications
{
    public class ApplyModel
    {
        [Required]
        public string DistanceId { get; set; }
    }
}

