using System.ComponentModel.DataAnnotations;

namespace MarathonApp.Models.Partners
{
    public static class DistanceModel
    {
        public class Base
        {
            [Required]
            public string Name { get; set; }

            [Required]
            public TimeSpan StartTime { get; set; }

            [Required]
            public TimeSpan PassingLimit { get; set; }

            [Required]
            [Range(0, 200, ErrorMessage = "Only positive number allowed")]
            public int AgeFrom { get; set; }

            [Required]
            [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
            public int NumberOfParticipants { get; set; }

            [Required]
            [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
            public int RegistredParticipants { get; set; }
        }
        
        public class BaseHasId : Base
        {
            [Required]
            public int Id { get; set; }
        }

        public class ListDistance : BaseHasId
        {
            public virtual ICollection<DistancePriceModel.ListDistancePrice> DistancePrices { get; set; }

            public virtual ICollection<DistanceAgeModel.ListDistanceAge> DistanceAges { get; set; }

            public int RemainingPlaces { get; set; }
        }

        public class AddDistance : Base
        {
            [Required]
            public ICollection<DistancePriceModel.AddDistancePrice> DistancePrices { get; set; }

            [Required]
            public ICollection<DistanceAgeModel.AddDistanceAge> DistanceAges { get; set; }
        }

        public class EditDistance : BaseHasId {
            public ICollection<DistancePriceModel.EditDistancePrice> DistancePrices { get; set; }

            public ICollection<DistanceAgeModel.EditDistanceAge> DistanceAges { get; set; }
        }
    }
}
