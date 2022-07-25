namespace MarathonApp.DAL.Models.Partner
{
    public static class PartnerDto
    {
        public class Base
        {
            public string Name { get; set; }
            public string Logo { get; set; }
            public string Url { get; set; }
        }
        public class BaseHasId: Base
        {
            public int Id { get; set; }
        }

        public class List: BaseHasId { }
        public class Get: BaseHasId { }
        public class Add: Base { }
        public class Edit: BaseHasId { }
    }
}
