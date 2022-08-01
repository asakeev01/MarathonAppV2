namespace MarathonApp.DAL.Entities
{
    public class Marathon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }

        public virtual ICollection<Partner>? Partners { get; set; }
        public virtual ICollection<Distance> Distances { get; set; }
    }
}
