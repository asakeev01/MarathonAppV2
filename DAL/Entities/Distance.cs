using System.ComponentModel.DataAnnotations.Schema;

namespace MarathonApp.DAL.Entities
{
    public class Distance
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan PassingLimit { get; set; }
        public int AgeFrom { get; set; }
        public int NumberOfParticipants { get; set; }
        public int RegistredParticipants { get; set; }
        [NotMapped]
        public int RemainingPlaces
        { 
            get
            {
                return NumberOfParticipants - RegistredParticipants;
            } 
        }
        public bool MedicalCertificate { get; set; }
        public virtual ICollection<DistancePrice> DistancePrices { get; set; }
        public virtual ICollection<DistanceAge> DistanceAges { get; set; }
        public virtual ICollection<Application> Applications { get; set; }
    }
}
