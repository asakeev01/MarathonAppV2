namespace MarathonApp.DAL.Entities
{
    public class Partner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string? Url { get; set; }

        public virtual ICollection<Marathon>? Marathons { get; set; }
    }
}
