namespace MarathonApp.Models.Partners
{
    public static class DistancePriceModel
    {
        public class Base
        {
            public DateTime DateStart { get; set; }
            public DateTime DateEnd { get; set; }
            public double Price { get; set; }
        }
        public class BaseHasId : Base
        {
            public int Id { get; set; }
        }

        public class ListDistancePrice : BaseHasId { }
        public class GetDistancePrice : BaseHasId { }
        public class AddDistancePrice : Base { }
        public class EditDistancePrice : BaseHasId { }
    }
}
