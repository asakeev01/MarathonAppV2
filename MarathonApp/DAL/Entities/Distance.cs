namespace MarathonApp.DAL.Entities
{
    public class Distance
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan PassingLimit { get; set; }
        public int AgeFrom { get; set; }

        public virtual ICollection<DistancePrice> DistancePrices { get; set; }
        public virtual ICollection<DistanceAge> DistanceAges { get; set; }
    }
}
