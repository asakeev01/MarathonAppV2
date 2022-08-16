namespace MarathonApp.DAL.Entities
{
    public class Partner
    {
        public int Id { get; set; }
        public int ImageId { get; set; }
        public virtual SavedFile Image { get; set; }
        public virtual ICollection<Marathon>? Marathons { get; set; }
    }
}
