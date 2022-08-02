using Mapster;
using MarathonApp.DAL.Entities;
using MarathonApp.Models.Partners;

namespace MarathonApp.Infrastructure;

internal static class MapsterProfile
{
    public static void Register()
    {
        #region Partners
        TypeAdapterConfig<Partner, PartnerModel.ListPartner>
            .NewConfig();
        TypeAdapterConfig<Partner?, PartnerModel.Get>
            .NewConfig();
        #endregion

        #region Marathons
        TypeAdapterConfig<Marathon, MarathonModel.ListMarathon>
            .NewConfig();
        TypeAdapterConfig<Marathon, MarathonModel.GetMarathon>
    .NewConfig();
        #endregion
    }
}
