using System.ComponentModel.DataAnnotations;

namespace MarathonApp.Models.Partners
{
    public static class PartnerModel
    {
        public class Base
        {
            [Required]
            public string Image { get; set; }
        }

        public class BaseHasId:Base
        {
            public int Id { get; set; }
        }

        public class ListPartner : BaseHasId { }

        public class Get: BaseHasId { }

        public class AddPartner: Base { }

        public class Edit: BaseHasId { }
    }
}
