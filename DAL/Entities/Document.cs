using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarathonApp.DAL.Entities
{
    public class Document
    {
        public Guid Id { get; set; }
        public string? FrontPassportPath { get; set; }
        public string? BackPassportPath { get; set; }
        public string? InsurancePath { get; set; }
        public string? DisabilityPath { get; set; }

        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}

