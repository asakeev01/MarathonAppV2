namespace MarathonApp.Models.Partners
{
    public static class DistanceAgeModel
    {
        public class Base
        {
            public int? AgeFrom { get; set; }
            public int? AgeTo { get; set; }
        }
        public class BaseHasId : Base
        {
            public int Id { get; set; }
        }

        public class ListDistanceAge : BaseHasId { }
        public class GetDistanceAge : BaseHasId { }
        public class AddDistanceAge : Base { }
        public class EditDistanceAge : BaseHasId { }
    }
}
