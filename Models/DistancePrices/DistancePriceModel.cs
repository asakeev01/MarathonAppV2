using System.ComponentModel.DataAnnotations;

namespace MarathonApp.Models.Partners
{
    public static class DistancePriceModel
    {
        public class Base
        {
            [Required]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public DateTime DateStart { get; set; }

            [Required]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public DateTime DateEnd { get; set; }

            [Required]
            [Range(0, double.MaxValue, ErrorMessage = "Only positive number allowed")]
            public double Price { get; set; }
        }

        public class BaseHasId : Base
        {
            [Required]
            public int Id { get; set; }
        }

        public class ListDistancePrice : BaseHasId { }

        public class GetDistancePrice : BaseHasId { }

        public class AddDistancePrice : Base { }

        public class EditDistancePrice : BaseHasId { }
    }
}
