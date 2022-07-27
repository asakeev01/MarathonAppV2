namespace MarathonApp.DAL.Entities
{
    public class DistancePrice
    {
        public int Id { get; set; }
        public DateTime DateStart {get; set;}
        public DateTime DateEnd { get; set; }
        public double Price { get; set; }

    }
}
