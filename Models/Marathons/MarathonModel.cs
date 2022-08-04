using Models.SavedFiles;

namespace MarathonApp.Models.Partners
{
    public static class MarathonModel
    {
        public class Base
        {
            public string Name { get; set; }
            public DateTime Date { get; set; }
            public string Text { get; set; }
            public DateTime StartDateAcceptingApplications { get; set; }
            public DateTime EndDateAcceptingApplications { get; set; }
            public bool IsActive { get; set; }
        }
        public class BaseHasId : Base
        {
            public int Id { get; set; }
        }

        public class MarathonDistancesPartners: BaseHasId
        {
            public ICollection<PartnerModel.ListPartner> Partners { get; set; }
            public ICollection<DistanceModel.ListDistance> Distances { get; set; }
        }
        public class ListMarathon : BaseHasId {
                    public ICollection<SavedFileModel.GetFiles> Images { get; set; }
        }
        public class GetMarathon : MarathonDistancesPartners {
            public ICollection<PartnerModel.ListPartner> Partners { get; set; }
            public ICollection<DistanceModel.GetDistance> Distances { get; set; }
            public ICollection<SavedFileModel.GetFiles> Images { get; set; }
        }
        public class AddMarathon : Base
        {
            public ICollection<DistanceModel.AddDistance> Distances { get; set; }
        }
        public class EditMarathon : BaseHasId { }

        public class EditMarathonDistance
        {
            public int Id { get; set; }
            public ICollection<DistanceModel.EditDistance> Distances { get; set; }
        }

        public class AddPartner
        {
            public int MarathonId { get; set; }
            public int PartnerId { get; set; }
        }
        public class RemovePartner: AddPartner { }

        public class DeleteImage
        {
            public int MarathonId { get; set; }
            public int ImageId { get; set; }
        }
    }
}
