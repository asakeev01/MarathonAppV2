using System.ComponentModel.DataAnnotations;

namespace MarathonApp.Models.Partners
{
    public static class DistanceAgeModel
    {
        public class Base
        {
            [Range(0, 200, ErrorMessage = "Only positive number allowed")]
            public int? AgeFrom { get; set; }

            [Range(0, 200, ErrorMessage = "Only positive number allowed")]
            public int? AgeTo { get; set; }
        }

        public class BaseHasId : Base
        {
            [Required]
            public int Id { get; set; }
        }

        public class ListDistanceAge : BaseHasId { }

        public class GetDistanceAge : BaseHasId { }

        public class AddDistanceAge : Base { }

        public class EditDistanceAge : BaseHasId { }
    }
}
