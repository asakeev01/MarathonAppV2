using Models.SavedFiles;
using System.ComponentModel.DataAnnotations;

namespace MarathonApp.Models.Partners
{
    public static class MarathonModel
    {
        public class Base
        {
            [Required]
            public string Name { get; set; }

            [Required]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public DateTime Date { get; set; }

            [Required]
            public string Text { get; set; }

            [Required]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public DateTime StartDateAcceptingApplications { get; set; }
            
            [Required]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public DateTime EndDateAcceptingApplications { get; set; }
            
            [Required]
            public bool IsActive { get; set; }
        }

        public class BaseHasId : Base
        {
            [Required]
            public int Id { get; set; }
        }

        public class ListMarathon : BaseHasId {
            public ICollection<SavedFileModel.GetFiles> Images { get; set; }
        }

        public class GetMarathon : ListMarathon
        {
            public ICollection<PartnerModel.ListPartner> Partners { get; set; }

            public ICollection<DistanceModel.ListDistance> Distances { get; set; }
        }

        public class AddMarathon : Base
        {
            public ICollection<DistanceModel.AddDistance> Distances { get; set; }
        }

        public class EditMarathon : BaseHasId { }

        public class EditMarathonDistance
        {
            [Required]
            public int Id { get; set; }

            [Required]
            public ICollection<DistanceModel.EditDistance> Distances { get; set; }
        }

        public class AddPartner
        {
            [Required]
            public int MarathonId { get; set; }

            [Required]
            public int PartnerId { get; set; }
        }

        public class DeletePartner: AddPartner { }

        public class DeleteImage
        {
            [Required]
            public int MarathonId { get; set; }
            
            [Required]
            public int ImageId { get; set; }
        }
    }
}
