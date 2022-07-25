namespace MarathonApp.DAL.Entities
{
    public class Marathon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string StartPlace { get; set; }
        public string FinishPlace { get; set; }
        public string Rules { get; set; }
        public string Awards { get; set; }

        public virtual ICollection<Partner> Partners { get; set; }
        public virtual ICollection<Distance> Distances { get; set; }
    }
}
