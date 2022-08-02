namespace MarathonApp.Models.Partners
{
    public static class DistanceModel
    {
        public class Base
        {
            public string Name { get; set; }
            public TimeSpan StartTime { get; set; }
            public TimeSpan PassingLimit { get; set; }
            public int AgeFrom { get; set; }
            public int NumberOfParticipants { get; set; }
            public int RegistredParticipants { get; set; }
        }
        public class BaseHasId : Base
        {
            public int Id { get; set; }
        }

        public class DistancePricesAges: BaseHasId
        {
            public virtual ICollection<DistancePriceModel.ListDistancePrice> DistancePrices { get; set; }
            public virtual ICollection<DistanceAgeModel.ListDistanceAge> DistanceAges { get; set; }
        }

        public class ListDistance : BaseHasId { }
        public class GetDistance : DistancePricesAges { }
        public class AddDistance : Base
        {
            public ICollection<DistancePriceModel.AddDistancePrice> DistancePrices { get; set; }
            public ICollection<DistanceAgeModel.AddDistanceAge> DistanceAges { get; set; }
        }
        public class EditDistance : BaseHasId {
            public ICollection<DistancePriceModel.EditDistancePrice> DistancePrices { get; set; }
            public ICollection<DistanceAgeModel.EditDistanceAge> DistanceAges { get; set; }
        }
    }
}
